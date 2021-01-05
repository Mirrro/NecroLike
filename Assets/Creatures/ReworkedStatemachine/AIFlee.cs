using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
    public class AIFlee : AIBehaviour
    {
        private void OnEnable()
        {
            entity.anim.SetTrigger("Flee");
        }

        private Transform startTransform;
        private void FixedUpdate()
        {
            if (!AIEntity.IsAlive(entity.GetNearestEnemy()))
            {
                finished.Invoke();
                return;
            }
            startTransform = entity.transform;
            entity.transform.rotation = Quaternion.LookRotation(entity.transform.position - entity.GetNearestEnemy().GetPosition());
            Vector3 runTo = entity.transform.position + entity.transform.forward * 1;
            NavMesh.SamplePosition(runTo, out NavMeshHit hit, 5, 1 << NavMesh.GetAreaFromName("Walkable"));
            entity.transform.position = startTransform.position;
            entity.transform.rotation = startTransform.rotation;
            entity.agent.SetDestination(hit.position);
        }
    }
}