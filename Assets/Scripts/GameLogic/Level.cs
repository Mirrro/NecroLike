using AIUnits;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Level : MonoBehaviour, IPlacementListener
{
    #region Level State
    public enum State { Entry, Positioning, Fighting, End };
    public State currentState = State.Entry;
    public UnityEvent<State> LevelStateBegin = new UnityEvent<State>();
    public UnityEvent<State> LevelStateEnd = new UnityEvent<State>();    

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
        Debug.Log(Game.ships[0].HasValue);
        Game.InitLevel(this);
        LevelGenerator.GenerateLevel(transform);
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
        LevelStateBegin.Invoke(state);
        currentState = state;
    }
    #endregion
    
    #region Units
    [SerializeField] protected Transform mobs;
    private List<AIEntity> vikings = new List<AIEntity>();
    private List<AIEntity> christians = new List<AIEntity>();

    public AIEntity[] GetEnemies(Game.Team enemyTeam)
    {
        if (enemyTeam == Game.Team.Christians)
            return christians.ToArray();
        return vikings.ToArray();
    }

    int placedShips;
    public void OnShipPlacement(ShipPlacementData data)
    {
        placedShips++;
        if (Game.NumberOfShips <= placedShips)
            GoToState(State.Fighting);
    }

    public void RegisterUnit(AIEntity creature)
    {
        LevelStateBegin.AddListener(creature.OnLevelStateBegin);
        creature.deathEvent.AddListener(UnregisterUnit);

        if (creature.team == Game.Team.Christians)
            christians.Add(creature);
        else
            vikings.Add(creature);
    }
    public void UnregisterUnit(AIEntity creature)
    {
        if (creature.team == Game.Team.Christians)
        {
            christians.Remove(creature);
            if (christians.Count <= 0)
                GoToState(State.End);
        }
        else
        {
            vikings.Remove(creature);
            if (vikings.Count <= 0)
                GoToState(State.End);
        }

    }
    #endregion
}
