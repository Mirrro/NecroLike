using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AIUnits
{
    public class ActionFlee : AIAction
    {
        public ActionFlee(AIBehaviour main) : base(main) { }
        private Transform startTransform;
        public override void Init()
        {
            main.entity.anim.SetTrigger("Flee");
        }
        public override bool Run()
        {
            if (main.entity.GetNearestEnemy() == null)
                return true;
            startTransform = main.entity.transform;
            main.entity.transform.rotation = Quaternion.LookRotation(main.entity.transform.position - main.entity.GetNearestEnemy().GetPosition());
            Vector3 runTo = main.entity.transform.position + main.entity.transform.forward * 1;
            NavMesh.SamplePosition(runTo, out NavMeshHit hit, 5, 1 << NavMesh.GetAreaFromName("Walkable"));
            main.entity.transform.position = startTransform.position;
            main.entity.transform.rotation = startTransform.rotation;
            main.entity.agent.SetDestination(hit.position);
            return false;
        }
    }
}