using AICreatures;
using UnityEngine;
using UnityEngine.Events;
using NecroCore.UI.INGAME;
public class Level : MonoBehaviour
{
    #region Singleton
    public static Level instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
        {
            instance = this;
        }
    }
    #endregion
    protected Game.GameState state = Game.GameState.Entry;
    protected Game.GameState nextState;
    protected float timeUntilNextState;
    [SerializeField]
    protected float transitionTime;
    [SerializeField]
    protected Transform[] stateCameraTransforms;

    [SerializeField] private float transitionSpeedMultiplier = 0.5f;
    private void Start()
    {
        UI_INGAME.AddToSkullScore(349);
        GameState = Game.GameState.Positioning;
        positioningShader.Set();
    }
    public static UnityEvent FightState = new UnityEvent();
    public static Game.GameState GameState
    {
        get
        {
            return instance.state;
        }
        set
        {
            if(instance.state!=value)
            {
                instance.nextState = value;
                instance.timeUntilNextState = instance.transitionTime;
            }
        }
    }
    private void Update()
    {
        if(state!=nextState)
        {
            timeUntilNextState -= Time.deltaTime * transitionSpeedMultiplier;
            float t = (transitionTime - timeUntilNextState)/ transitionTime;
            t = t * t * (3f - 2f * t);
            if (timeUntilNextState <= 0)
            {
                timeUntilNextState = 0;
                state = nextState;
                if (instance.state == Game.GameState.Fighting)
                {
                    FightState.Invoke();
                    pingShader.Set();

                }
            }
            Camera.main.transform.position = Vector3.Lerp(stateCameraTransforms[(int)state].position, stateCameraTransforms[(int)nextState].position, t);
            Camera.main.transform.rotation = Quaternion.Lerp(stateCameraTransforms[(int)state].rotation, stateCameraTransforms[(int)nextState].rotation, t);
            if(nextState == Game.GameState.Positioning)
                positioningShader.ShaderLerpRadius(t);
            else if(nextState == Game.GameState.Fighting)
                positioningShader.ShaderLerpRadius(1-t);
        }
        if(pingAnimTimer>0)
        {
            pingAnimTimer -= Time.deltaTime;
            pingShader.ShaderLerpRadius(pingAnimTimer - pingAnimTimer * pingAnimTimer);
        }
    }
    private float pingAnimTimer;
    public static void Rally(Vector3 position)
    {
        instance.pingAnimTimer = 1;
        instance.pingShader.SetPosition(position);
        int layerMask = 1 << 6;
        Collider[] hitColliders = Physics.OverlapSphere(position, 10, layerMask);
        foreach (Collider collider in hitColliders)
        {
            AIRally rally = collider.GetComponent<AIRally>();
            if (rally != null)
                rally.Rally(position);
        };
    }
    #region Shader Handeling

    [SerializeField]
    private ShaderHandler positioningShader;
    [SerializeField]
    private ShaderHandler pingShader;

    #endregion
    #region Creature Tracking
    [SerializeField]
    protected Transform mobs;
    public static Transform Mobs
    {
        get
        {
            return instance.mobs;
        }
    }
    private int[] creatureCount = new int[2] { 0, 0 };

    public static void RegisterCreature(int team)
    {
        instance.creatureCount[team]++;
        if(team == (int)Game.Team.Humans)
            UI_SpawnCreaturePanel.UpdateHumanCount(instance.creatureCount[team]);
    }

    public static void UnregisterCreature(int team)
    {
        instance.creatureCount[team]--;
        if (team == (int)Game.Team.Humans)
        {
            UI_SpawnCreaturePanel.UpdateHumanCount(instance.creatureCount[team]);
            if (instance.creatureCount[team] <= 0)
                Game.LevelFinished();
        }
    }
    #endregion
}
