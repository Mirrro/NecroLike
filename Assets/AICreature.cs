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

        [Header("Targeting")]
        public List<int> targets = new List<int>();

        [Header("Combat Attributes")]
        public float range;
        public float attackAnimationLength;
        public float attackPrepareTime;
        public float attackCooldown;
        public int health;
        public int damage;

        #region Statemachine
        private void Start()
        {
            ID = Game.RegisterCreature(this);
            InitState(entryState);
            InitState(defaultState);
            InitState(targetFoundState);
            InitState(deathState);
            ChangeState(entryState);
            if (!IsAlive())
                Death();
        }

        private void LateUpdate()
        {
            if (currentState != null)
                currentState.Update();

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
            if (GetTarget() == null)
                ChangeState(defaultState);
            else
                ChangeState(targetFoundState);

        }
        #endregion

        #region TargetHandling

        public virtual void TargetFound(AICreature target)
        {
            targets.Add(target.ID);

            if (currentState.GetID() != (int)targetFoundState)
                ChangeState(targetFoundState);
        }
        public void TargetLost(AICreature target)
        {
            targets.Remove(target.ID);

            if (GetTarget() == null && currentState.GetID() == (int)targetFoundState)
                ChangeState(defaultState);
        }
        public void OnTriggerEnter(Collider other)
        {
            AICreature creature = other.GetComponent<AICreature>();
            if (creature == null)
                return;
            if (creature.team!=team && creature.IsAlive())
                TargetFound(creature);    
        }
        public void OnTriggerExit(Collider other)
        {
            AICreature creature = other.GetComponent<AICreature>();
            if (creature == null)
                return;
            if (creature.team != team)
                TargetLost(creature);
        }
        public AICreature GetTarget()
        {
            for(int i = 0; i<targets.Count; i++)
            {
                AICreature target = Game.instance.GetCreature(team == 0 ? 1 : 0, targets[i]);
                if (IsValidTarget(target))
                    return target;
            }
                
            
            return null;
        }
        public bool IsValidTarget(AICreature target)
        {
            if (target == null)
                return false;
            if (!target.IsAlive())
                return false;
            if (IsObstructed(target))
                return false;
            if (tag == "Skeleton" && Vector3.Distance(transform.position, target.transform.position) > 3)
                return false;
            return true;
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
            Game.UnregisterCreature(this);
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

        public bool IsObstructed(AICreature target)
        {
            if (target == null)
                return true;
            int layerMask = 1 << 6;
            layerMask = ~layerMask;

            RaycastHit hit;
            Debug.DrawRay(transform.position, (target.transform.position - transform.position) * Vector3.Distance(transform.position, target.transform.position), Color.yellow);
            if (Physics.Raycast(transform.position, target.transform.position - transform.position , out hit, Vector3.Distance(transform.position, target.transform.position), layerMask))
                return true;
            else
                return false;
        }
        #endregion

        public void PrintForMe(string str)
        {
            print(str);
        }
    }
}