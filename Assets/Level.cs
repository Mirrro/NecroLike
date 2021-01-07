using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AICreatures;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class Level : MonoBehaviour
{
    #region Singleton
    public static Level instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
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
    private void Start()
    {
        GameState = Game.GameState.Positioning;
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
            timeUntilNextState -= Time.deltaTime;
            float t = (transitionTime - timeUntilNextState)/ transitionTime;
            t = t * t * (3f - 2f * t);
            if (timeUntilNextState <= 0)
            {
                timeUntilNextState = 0;
                state = nextState;
                if (instance.state == Game.GameState.Fighting)
                    FightState.Invoke();
            }
            Camera.main.transform.position = Vector3.Lerp(stateCameraTransforms[(int)state].position, stateCameraTransforms[(int)nextState].position, t);
            Camera.main.transform.rotation = Quaternion.Lerp(stateCameraTransforms[(int)state].rotation, stateCameraTransforms[(int)nextState].rotation, t);
        }
    }
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
            IngameUI.UpdateHumanCount(instance.creatureCount[team]);
    }

    public static void UnregisterCreature(int team)
    {
        instance.creatureCount[team]--;
        if (team == (int)Game.Team.Humans)
        {
            IngameUI.UpdateHumanCount(instance.creatureCount[team]);
            if (instance.creatureCount[team] <= 0)
                Game.LevelFinished();
        }
    }
    #endregion
}
