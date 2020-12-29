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

    public abstract class AIState
    {
        protected AICreature main;

        public abstract int GetID();

        public void Init(AICreature main)
        {
            this.main = main;
        }

        public abstract void Enter();

        public abstract void Exit();

        public abstract void Update();
    }

    public class AIStateIdle : AIState
    {
        private float timeLeft;
        public static int ID = 0;
        public override int GetID()
        {
            return ID;
        }
        public override void Enter()
        {
            timeLeft = Random.Range(1, 5);
        }

        public override void Exit()
        {

        }

        public override void Update()
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
                main.FinishedState(ID);
        }
    }

    public class AIStateWander : AIState
    {
        public static int ID = 1;
        public override int GetID()
        {
            return ID;
        }
        public override void Enter()
        {
            NavMesh.SamplePosition(Random.insideUnitSphere * 5 + main.transform.position, out NavMeshHit hit, 5, NavMesh.AllAreas);
            main.agent.SetDestination(hit.position);
        }

        public override void Update()
        {
            if (main.agent.remainingDistance <= main.agent.stoppingDistance)
                main.FinishedState(ID);
        }
        public override void Exit()
        {
            main.agent.destination = main.transform.position;
        }
    }

    public class AIStateFlee : AIState
    {
        public static int ID = 2;
        public override int GetID()
        {
            return ID;
        }
        Transform threat;
        float nextTurnTime;
        private Transform startTransform;
        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            startTransform = main.transform;
            main.transform.rotation = Quaternion.LookRotation(main.transform.position - main.GetTarget().transform.position);
            Vector3 runTo = main.transform.position + main.transform.forward * 1;
            NavMeshHit hit;   
            NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetAreaFromName("Walkable"));
            nextTurnTime = Time.time + 5;
            main.transform.position = startTransform.position;
            main.transform.rotation = startTransform.rotation;
            main.agent.SetDestination(hit.position);
        }
    }

    public class AIStateChase : AIState
    {
        public static int ID = 3;
        public override int GetID()
        {
            return ID;
        }
        public override void Enter()
        {
            main.agent.destination = main.GetTarget().transform.position;
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            if (main.agent.remainingDistance <= main.range)
                main.FinishedState(ID);

            else
                main.agent.destination = main.GetTarget().transform.position;
        }
    }

    public class AIStateFight : AIState
    {
        public static int ID = 4;
        public override int GetID()
        {
            return ID;
        }
        float attackPreparation;
        public override void Enter()
        {
            attackPreparation = 1 / main.attackspeed;
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            if (Vector3.Distance(main.GetTarget().transform.position, main.transform.position) <= main.range)
            {
                if (attackPreparation <= 0)
                    Attack();
                else
                    attackPreparation -= Time.deltaTime;
            }
            else
                main.FinishedState(ID);
        }

        private void Attack()
        {
            attackPreparation = 1 / main.attackspeed;
            if (Vector3.Distance(main.GetTarget().transform.position, main.transform.position) <= main.range)
                main.GetTarget().GetHit(main);
        }
    }
}