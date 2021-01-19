using AICreatures;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Level : MonoBehaviour
{
    public enum State { Entry, Positioning, Fighting, End };
    public  State currentState = State.Entry;
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
        LevelStateEnd.Invoke(currentState);
        for (float t = 0f; t < 1; t += Time.deltaTime*Game.level.transitionSpeed)
        {
            yield return null;
        }
        print(state.ToString());
        LevelStateBegin.Invoke(state);
        currentState = state;
    }



    #region Creatures
    [SerializeField] protected Transform mobs;
    private List<AIEntity> monsters = new List<AIEntity>();
    private List<AIEntity> humans = new List<AIEntity>();

    public AIEntity[] GetEnemies(Game.Team enemyTeam)
    {
        if (enemyTeam == Game.Team.Humans)
            return humans.ToArray();
        return monsters.ToArray();
    }

    public AIEntity[] inGameLoadout = new AIEntity[Game.loadout.Length];
    int placedCreatures = 0;
    public void OnCreaturePlacement(CreaturePlacementData data)
    {
        AIEntity creature = Instantiate(Game.loadout[data.creature].prefab, data.position, Quaternion.identity, mobs).GetComponent<AIEntity>();
        creature.stats = Game.loadout[data.creature].stats;
        inGameLoadout[data.creature] = creature;

        placedCreatures++;
        if (placedCreatures >= Game.AvailableCreatures)
            GoToState(State.Fighting);
    }
    public void RegisterCreature(AIEntity creature)
    {
        print("reg");
        LevelStateBegin.AddListener(creature.OnLevelStateBegin);
        creature.deathEvent.AddListener(UnregisterCreature);

        if (creature.team == Game.Team.Humans)
            humans.Add(creature);
        else
            monsters.Add(creature);
    }
    public void UnregisterCreature(AIEntity creature)
    {
        if (creature.team == Game.Team.Humans)
        {
            humans.Remove(creature);
            if (humans.Count <= 0)
                GoToState(State.End);
        }
        else
        {
            monsters.Remove(creature);
            if (monsters.Count <= 0)
                GoToState(State.End);
        }

    }
    #endregion
}
