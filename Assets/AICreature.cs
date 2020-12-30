﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

namespace AICreatures
{
    public class AICreature : MonoBehaviour
    {
        [Header("State Machine")]
        public AIState currentState;
        public Dictionary<int, AIState> states = new Dictionary<int, AIState>();

        [Header("Base Mob Components")]
        public NavMeshAgent agent;
        public UnityEvent deathEvent = new UnityEvent();

        [Header("Targeting")]
        public List<AICreature> targets = new List<AICreature>();
        public AICreature lastTarget;
        public string targetTag;

        [Header("Combat Attributes")]
        public float range;
        public float attackspeed;
        public int health;
        public int damage;

        #region Statemachine
        private void FixedUpdate()
        {
            if (currentState != null)
                currentState.Update();
        }
        public virtual void InitState(AIState state)
        {
            states.Add(state.GetID(), state);
            state.Init(this);
        }
        public void ChangeState(int state)
        {
            if (currentState != null)
                currentState.Exit();
            states.TryGetValue(state, out currentState);
            if (currentState != null)
                currentState.Enter();
        }
        public virtual void FinishedState(int state)
        {
        }
        #endregion

        #region TargetHandling
        public void TrimTargets()
        {
            List<AICreature> deadTargets = new List<AICreature>();

            foreach (AICreature target in targets)
                if (target == null || !target.IsAlive())
                    deadTargets.Add(target);

            foreach (AICreature target in deadTargets)
                targets.Remove(target);

            targets.TrimExcess();
        }
        public virtual void TargetFound(AICreature target)
        {
            target.deathEvent.AddListener(TrimTargets);
            targets.Add(target);
        }
        public void TargetLost(AICreature target)
        {
            target.deathEvent.RemoveListener(TrimTargets);
            targets.Remove(target);
        }
        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == targetTag && IsAlive())
                TargetFound(other.GetComponent<AICreature>());    
        }
        public void OnTriggerExit(Collider other)
        {
            if (other.tag == targetTag && IsAlive())
                TargetLost(other.GetComponent<AICreature>());
        }
        public AICreature GetTarget()
        {
            if (lastTarget == null && targets.Count == 0)
                return null;
            if (lastTarget == null || !lastTarget.IsAlive())
            {
                for(int i = 0; i<targets.Count; i++)
                    if (!IsObstructed(targets[i]))
                        return(lastTarget = targets[i]);
            }
            return lastTarget;
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
            currentState = null;
        }

        public bool IsValidTarget(AICreature target)
        {
            return target!=null && IsAlive() && !IsObstructed(target);
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