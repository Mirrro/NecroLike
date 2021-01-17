using AICreatures;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Level : MonoBehaviour
{
    public enum State { Entry, Positioning, Fighting, End };
    protected  State currentState = State.Entry;
    public UnityEvent<State> LevelStateBegin = new UnityEvent<State>();
    public UnityEvent<State> LevelStateEnd = new UnityEvent<State>();
    public void InitLevelStateListener(ILevelStateListener listener)
    {
        LevelStateBegin.AddListener(listener.OnLevelStateBegin);
        LevelStateEnd.AddListener(listener.OnLevelStateEnd);
    }

    [SerializeField] protected float transitionSpeed;
    public static float TransitionSpeed {
        get
        {
            return Game.level.transitionSpeed;
        }
    }

    private void Awake()
    {
        if (FindObjectOfType<Loader>() == null)
        {
            Game.Load();
            return;
        }
        Game.InitLevel(this);
        InputHandler.PositionCreatureEvent.AddListener(OnCreaturePlacement);
    }


    public void GoToState(State state)
    {
        if(state!=currentState)
            StartCoroutine(TransitionState(state));
    }

    public IEnumerator TransitionState(State state)
    {
        print(state.ToString());
        LevelStateEnd.Invoke(currentState);
        for (float t = 0f; t < 1; t += Time.deltaTime*Game.level.transitionSpeed)
        {
            yield return null;
        }
        LevelStateBegin.Invoke(state);
        currentState = state;
    }

    #region Player Input
    public AIEntity[] inGameLoadout = new AIEntity[Game.loadout.Length];
    int placedCreatures = 0;
    public void OnCreaturePlacement(CreaturePlacementData data)
    {
        AIEntity creature = Instantiate(Game.loadout[data.creature].prefab, data.position, Quaternion.identity, mobs).GetComponent<AIEntity>();
        InitLevelStateListener(creature);
        creature.stats = Game.loadout[data.creature].stats;
        inGameLoadout[data.creature] = creature;

        placedCreatures++;
        if (placedCreatures >= Game.AvailableCreatures)
            GoToState(State.Fighting);
    }
    [SerializeField] protected Transform mobs;


    public UnityEvent<Vector3> rallyEvent = new UnityEvent<Vector3>();
    public void Rally(Vector3 position)
    {
        rallyEvent.Invoke(position);
        int layerMask = 1 << 6;
        Collider[] hitColliders = Physics.OverlapSphere(position, 5, layerMask);
        foreach (Collider collider in hitColliders)
        {
            AIRally rally = collider.GetComponent<AIRally>();
            if (rally != null)
                rally.Rally(position);
        };
    }
    #endregion

    #region Creature Tracking
    private int humanCount;
    private int undeadCount;
    public void CountDown(Game.Team team)
    {
        if (team == Game.Team.Humans)
        {
            humanCount--;
            if (humanCount <= 0)
                GoToState(State.End);
        }
        else
        {
            undeadCount--;
            if(undeadCount <= 0)
                GoToState(State.End);
        }
    }
    public void CountUp(Game.Team team)
    {
        if (team == Game.Team.Humans)
            humanCount++;
        else
            undeadCount++;
    }
    #endregion
}
