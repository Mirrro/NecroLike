using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

namespace AICreatures
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AICreature : MonoBehaviour, ITargetable
    {
        private ITargetable target;
        [Header("Base Components")]
        public NavMeshAgent agent;
        public Animator anim;

        [Header("AI SETTINGS")]
        [HideInInspector]
        public int ID;
        public Game.Team team;
        public Game.Team enemyTeam;

        [Header("States")]
        public AIManager.AIStateType entryState;
        public AIManager.AIStateType defaultState;
        public AIManager.AIStateType targetFoundState;
        public AIManager.AIStateType deathState;

        public AIManager.AIStateType currentStateType;
        private AIState currentState;
        private Dictionary<AIManager.AIStateType, AIState> states = new Dictionary<AIManager.AIStateType, AIState>();

        public UnityEvent deathEvent = new UnityEvent();
        bool forcedState;

        [Header("Stats")]
        public float vision;
        public float range;
        public float attackAnimationLength;
        public float attackPrepareTime;
        public float attackCooldown;
        public int health;
        public int damage;

        #region Statemachine
        private void Start()
        {
            Game.RegisterCreature((int)team);
            InitState(entryState);
            InitState(defaultState);
            InitState(targetFoundState);
            InitState(deathState);
            ChangeState(entryState);
            if (!IsAlive())
                Death();
        }

        public void ForceState(AIManager.AIStateType state)
        {
            InitState(state);

            ChangeState(state);
            forcedState = true;
        }

        private void FixedUpdate()
        {
            if (IsAlive() && !forcedState)
            {
                UpdateTarget();
                if (GetTarget() != null && currentStateType != targetFoundState)
                    ChangeState(targetFoundState);
                else if (GetTarget() == null && currentStateType == targetFoundState)
                    ChangeState(defaultState);
            }
            if (currentState != null)
                currentState.Update();
        }
        private void Update()
        {
            if (currentState != null)
                currentState.VisualUpdate();
        }
        public void InitState(AIManager.AIStateType stateType)
        {
            if(!states.ContainsKey(stateType))
            {
                AIState state = AIManager.ActivateState(stateType);
                states.Add(stateType, state);
                state.Init(this);
            }
        }
        public void ChangeState(AIManager.AIStateType state)
        {
            currentStateType = state;

            if (currentState != null)
                currentState.Exit();
            states.TryGetValue(state, out currentState);
            if (currentState != null)
                currentState.Enter();
        }
        public virtual void FinishedState(int state)
        {
            if (forcedState)
                forcedState = false;
            if (state == (int)deathState)
            {
                currentState.Exit();
                Destroy(gameObject);
            }
            else if (state == (int)entryState)
                ChangeState(defaultState);

        }
        #endregion

        #region TargetHandling
        private void UpdateTarget()
        {
            int layerMask = 1 << (6 + ((int)enemyTeam));
            Collider[] hitColliders = Physics.OverlapSphere(GetPosition(), vision, layerMask);

            layerMask = ~layerMask;
            Collider closestCollider = null;
            foreach (var hitCollider in hitColliders)
            {
                RaycastHit hit;
                if (!Physics.Raycast(transform.position, hitCollider.transform.position - transform.position, out hit, Vector3.Distance(transform.position, hitCollider.transform.position), layerMask))
                {
                    if (closestCollider == null || Vector3.Distance(transform.position, hitCollider.transform.position) < Vector3.Distance(transform.position, closestCollider.transform.position))
                        closestCollider = hitCollider;
                }

            }
            if (closestCollider == null)
                target = null;
            else
                target = closestCollider.GetComponent<ITargetable>();
        }
        public ITargetable GetTarget()
        {
            return target;
        }

        #endregion

        #region MobBehaviour
        public void GetHit(int damage)
        {
            health -= damage;
            if (health <= 0)
                Death();
        }

        public virtual void Death()
        {
            Game.UnregisterCreature((int)team);
            deathEvent.Invoke();
            gameObject.layer = 0;
            ChangeState(deathState);
        }
        
        public bool IsInRange(Vector3 position)
        {
            return (Vector3.Distance(position, transform.position) <= range);
        }

        public bool IsAlive()
        {
            return this != null && isActiveAndEnabled && health > 0;
        }

       
        #endregion

        public void PrintForMe(string str)
        {
            print(str);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}