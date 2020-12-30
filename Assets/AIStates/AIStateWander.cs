using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
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
}