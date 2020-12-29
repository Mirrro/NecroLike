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
        public AICreature target;

        public float range;
        public float attackspeed;
        public int health;
        public int damage;

        public virtual void InitState(AIState state)
        {
            print("initing " + name + " " + state.GetID());
            states.Add(state.GetID(), state);
            state.Init(this);
        }

        public void ChangeState(int state)
        {
            print(name + " " + state);
            if(currentState!=null)
                currentState.Exit();
            states.TryGetValue(state, out currentState);
            if (currentState != null)
                currentState.Enter();
        }

        public virtual void FinishedState(int state)
        {
            print(name + " " + state);
        }

        private void FixedUpdate()
        {
            if(currentState!=null)
                currentState.Update();
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
            main.transform.rotation = Quaternion.LookRotation(main.transform.position - main.target.transform.position);
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
        bool attacking;
        AICreature target;
        public override void Enter()
        {
            target = main.target;
            main.agent.destination = target.transform.position;
        }

        public override void Exit()
        {
            LEAVE TO ATTACK WHEN IN RANGE
        }

        public override void Update()
        {
            if (main.agent.remainingDistance <= main.range)
                main.FinishedState(ID);

            else
                main.agent.destination = target.transform.position;
        }
    }

    public class AIStateFight : AIState
    {
        public static int ID = 4;
        public override int GetID()
        {
            return ID;
        }
        AICreature target;
        public override void Enter()
        {
            target = main.target;

        }

        public override void Exit()
        {
            LEAVE TO CHASE WHEN OUT OF RANGE
        }

        public override void Update()
        {

        }
    }
}