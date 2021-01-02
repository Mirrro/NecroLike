using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
    public class AIStateFlee : AIState
    {
        AICreature target;
        public override void Enter()
        {
            target = main.GetTarget();
            main.anim.SetTrigger("Flee");
        }

        public override void Exit()
        {
        }

        private Transform startTransform;
        public override void Update()
        {
            startTransform = main.transform;
            main.transform.rotation = Quaternion.LookRotation(main.transform.position - target.transform.position);
            Vector3 runTo = main.transform.position + main.transform.forward * 1;
            NavMeshHit hit;
            NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetAreaFromName("Walkable"));
            main.transform.position = startTransform.position;
            main.transform.rotation = startTransform.rotation;
            main.agent.SetDestination(hit.position);
        }
    }
}