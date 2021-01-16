using AICreatures;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.EventSystems;

public class Level : MonoBehaviour
{
    public enum State { Entry, Positioning, Fighting, End };
    protected  State currentState = State.Entry;
    public static UnityEvent<State> StateBegin = new UnityEvent<State>();
    public static UnityEvent<State> StateEnd = new UnityEvent<State>();
    public static void InitStateListener(ILevelStateListener listener)
    {
        StateBegin.AddListener(listener.OnStateBegin);
        StateEnd.AddListener(listener.OnStateEnd);
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
        StateEnd.Invoke(currentState);
        for (float t = 0f; t < 1; t += Time.deltaTime*Game.level.transitionSpeed)
        {
            yield return null;
        }
        StateBegin.Invoke(state);
        currentState = state;
    }

    int placedCreatures = 0;
    public void OnCreaturePlacement(CreaturePlacementData data)
    {
        SpawnMob(Game.loadout[data.creature], data.position);
        placedCreatures++;
        if (placedCreatures >= Game.loadout.Length)
            GoToState(State.Fighting);
    }
    [SerializeField] protected Transform mobs;
    public void SpawnMob(GameObject mob, Vector3 position)
    {
        Instantiate(mob, position, Quaternion.identity, mobs);
    }
    public UnityEvent<Vector3> rallyEvent = new UnityEvent<Vector3>();
    public void Rally(Vector3 position)
    {
        rallyEvent.Invoke(position);
        int layerMask = 1 << 6;
        Collider[] hitColliders = Physics.OverlapSphere(position, 10, layerMask);
        foreach (Collider collider in hitColliders)
        {
            AIRally rally = collider.GetComponent<AIRally>();
            if (rally != null)
                rally.Rally(position);
        };
    }

    private int humanCount;
    public void CountDown(Game.Team team)
    {
        if (team == Game.Team.Humans)
        {
            humanCount--;
            if (humanCount <= 0)
                GoToState(State.End);
        }
    }
    public void CountUp(Game.Team team)
    {
        if (team == Game.Team.Humans)
            humanCount++;
    }

}
