using AICreatures;
using UnityEngine;
using UnityEngine.Events;
using NecroCore.UI.INGAME;
using System.Collections;
using System.Linq;

public class Level : MonoBehaviour
{
    #region Singleton
    private void Awake()
    {
        Game.level = this;
    }
    #endregion

    public enum State { Entry, Positioning, Fighting, End };
    protected static State currentState = State.Entry;
    public static UnityEvent<State> stateBegin = new UnityEvent<State>();
    public static UnityEvent<State> stateEnd = new UnityEvent<State>();

    public static UnityEvent<Vector3> rallyEvent = new UnityEvent<Vector3>();
    [SerializeField] protected float transitionSpeed;
    public static float TransitionSpeed {
        get
        {
            return Game.level.transitionSpeed;
        }
    }

    private void Start()
    {
        if (FindObjectOfType<Loader>() == null)
            Game.Load();

        var listeners = FindObjectsOfType<MonoBehaviour>().OfType<ILevelStateListener>();
        foreach (ILevelStateListener s in listeners)
        {
            stateBegin.AddListener(s.OnStateBegin);
            stateEnd.AddListener(s.OnStateEnd);
        }
    }
    public static IEnumerator GoToState(State state)
    {
        stateEnd.Invoke(currentState);
        for (float t = 0f; t < 1; t += Time.deltaTime*Game.level.transitionSpeed)
        {
            yield return null;
        }
        stateBegin.Invoke(state);
        currentState = state;
    }
    
    public static void Rally(Vector3 position)
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

    #region Creature Tracking
    [SerializeField]
    protected Transform mobs;
    public static void SpawnMob(GameObject mob, Vector3 position)
    {
        Instantiate(mob, position, Quaternion.identity, Mobs).GetComponent<AIEntity>();
    }
    public static Transform Mobs
    {
        get
        {
            return Game.level.mobs;
        }
    }
    private int[] creatureCount = new int[2] { 0, 0 };

    public static void RegisterCreature(int team)
    {
        Game.level.creatureCount[team]++;
        if(team == (int)Game.Team.Humans)
            UI_SpawnCreaturePanel.UpdateHumanCount(Game.level.creatureCount[team]);
    }

    public static void UnregisterCreature(int team)
    {
        Game.level.creatureCount[team]--;
        if (team == (int)Game.Team.Humans)
        {
            UI_SpawnCreaturePanel.UpdateHumanCount(Game.level.creatureCount[team]);
        }
    }
    #endregion
}
