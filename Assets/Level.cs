using AICreatures;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.EventSystems;

public class Level : MonoBehaviour
{
    public enum State { Entry, Positioning, Fighting, End };
    protected  State currentState = State.Entry;
    public  UnityEvent<State> StateBegin = new UnityEvent<State>();
    public  UnityEvent<State> StateEnd = new UnityEvent<State>();

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
            Game.Load();
        Game.InitLevel(this);

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            Game.FireInput();
    }

    public  IEnumerator GoToState(State state)
    {
        StateEnd.Invoke(currentState);
        for (float t = 0f; t < 1; t += Time.deltaTime*Game.level.transitionSpeed)
        {
            yield return null;
        }
        StateBegin.Invoke(state);
        currentState = state;
    }

    #region Player Input
    public void OnInput(Vector3 input)
    {
        if (currentState == State.Positioning && selectedCreature != -1)
            SpawnMob(Game.loadout[selectedCreature], input);
        else if (currentState == State.Fighting)
            Rally(input);
    }

    public int selectedCreature;
    public void OnCreatureSelected(int creature)
    {
        selectedCreature = creature;
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

    [SerializeField] protected Transform mobs;
    public void SpawnMob(GameObject mob, Vector3 position)
    {
        Instantiate(mob, position, Quaternion.identity, mobs);
    }
    #endregion



}
