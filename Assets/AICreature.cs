using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

namespace AICreatures
{
    public class AICreature : MonoBehaviour
    {
        public int ID;
        public int team;
        public int enemyTeam;

        [Header("State Machine")]
        public AIManager.AIStateType entryState;
        public AIManager.AIStateType defaultState;
        public AIManager.AIStateType targetFoundState;
        public AIManager.AIStateType deathState;

        public AIManager.AIStateType currentStateType;
        public AIState currentState;
        public Dictionary<AIManager.AIStateType, AIState> states = new Dictionary<AIManager.AIStateType, AIState>();

        [Header("Base Mob Components")]
        public NavMeshAgent agent;
        public Animator anim;
        public UnityEvent deathEvent = new UnityEvent();

        [Header("Combat Attributes")]
        public float vision;
        public float range;
        public float attackAnimationLength;
        public float attackPrepareTime;
        public float attackCooldown;
        public int health;
        public int damage;
        public int targetCreatureID;

        #region Statemachine
        private void Start()
        {
            InitState(entryState);
            InitState(defaultState);
            InitState(targetFoundState);
            InitState(deathState);
            ChangeState(entryState);
            if (!IsAlive())
                Death();
        }

        private void FixedUpdate()
        {
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
            if (state == (int)deathState)
            {
                currentState.Exit();
                Destroy(gameObject);
            }
            else if (GetTarget() == null)
                ChangeState(defaultState);
            else
                ChangeState(targetFoundState);

        }
        #endregion

        #region TargetHandling
        public AICreature GetTarget()
        {
            int layerMask = 1 << (6+team);
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, vision, layerMask);

            layerMask = ~layerMask;
            Collider closestCollider = null;
            foreach (var hitCollider in hitColliders)
            {
                RaycastHit hit;
                if (!Physics.Raycast(transform.position, hitCollider.transform.position - transform.position, out hit, Vector3.Distance(transform.position, hitCollider.transform.position), layerMask))
                {
                    if (closestCollider == null|| Vector3.Distance(transform.position, hitCollider.transform.position)< Vector3.Distance(transform.position, closestCollider.transform.position))
                        closestCollider = hitCollider;
                }

            }
            if (closestCollider == null)
                return null;
            return closestCollider.GetComponent<AICreature>();
        }

        #endregion

        #region MobBehaviour
        public void GetHit(AICreature damager)
        {
            health -= damager.damage;
            if (health <= 0)
                Death();
        }

        public virtual void Death()
        {
            deathEvent.Invoke();
            gameObject.layer = 0;
            ChangeState(deathState);
        }
        
        public bool IsInRange(AICreature target)
        {
            return (Vector3.Distance(target.transform.position, transform.position) <= range);
        }

        public bool IsAlive()
        {
            return isActiveAndEnabled && health > 0;
        }

       
        #endregion

        public void PrintForMe(string str)
        {
            print(str);
        }
    }
}