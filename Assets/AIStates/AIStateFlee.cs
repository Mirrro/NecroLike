using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
    public class AIStateFlee : AIState
    {
        public static int ID = 2;
        public override int GetID()
        {
            return ID;
        }

        AICreature target;
        public override void Enter()
        {
            target = main.GetTarget();
        }

        public override void Exit()
        {
        }

        private Transform startTransform;
        float nextTurnTime;
        public override void Update()
        {
            if (main.IsValidTarget(target))
            {
                startTransform = main.transform;
                main.transform.rotation = Quaternion.LookRotation(main.transform.position - target.transform.position);
                Vector3 runTo = main.transform.position + main.transform.forward * 1;
                NavMeshHit hit;
                NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetAreaFromName("Walkable"));
                nextTurnTime = Time.time + 5;
                main.transform.position = startTransform.position;
                main.transform.rotation = startTransform.rotation;
                main.agent.SetDestination(hit.position);
            }
            else
                main.FinishedState(ID);
            
        }
    }
}