using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
    public class AICreature : MonoBehaviour
    {
        public AIState currentState;
        public Dictionary<int,AIState> states = new Dictionary<int, AIState>();
        public NavMeshAgent agent;
        public List<AICreature> targets = new List<AICreature>();
        private AICreature lastTarget;
        public string targetTag;

        public float range;
        public float attackspeed;
        public int health;
        public int damage;

        public virtual void InitState(AIState state)
        {
            states.Add(state.GetID(), state);
            state.Init(this);
        }

        public void ChangeState(int state)
        {
            if(currentState!=null)
                currentState.Exit();
            states.TryGetValue(state, out currentState);
            if (currentState != null)
                currentState.Enter();
        }

        public virtual void FinishedState(int state)
        {
        }

        private void FixedUpdate()
        {
            if(currentState!=null)
                currentState.Update();
        }

        public void GetHit(AICreature damager)
        {
            health -= damager.damage;
            if (health < 0)
            {
                damager.lastTarget = null;
                damager.targets.Remove(this);
                Death();
            }
        }

        public virtual void Death()
        {
            currentState = null;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == targetTag)
            {
                if(lastTarget==null||lastTarget.health<0)
                    lastTarget = other.GetComponent<AICreature>();
                TargetFound();
                targets.Add(other.GetComponent<AICreature>());
            }
        }
        public void OnTriggerExit(Collider other)
        {
            if (other.tag == targetTag)
            {
                targets.Remove(other.GetComponent<AICreature>());
            }
        }
        public virtual void TargetFound()
        {

        }
        public AICreature GetTarget()
        {
            if (lastTarget == null && targets.Count == 0)
                return null;
            if (lastTarget == null || lastTarget.health<0)
            {
                for(int i = 0; i<targets.Count; i++)
                {
                    if(targets[i].gameObject.activeSelf)
                        lastTarget = targets[i];
                }
            }
            return lastTarget;
        }
        public void PrintForMe(string str)
        {
            print(str);
        }
    }
}