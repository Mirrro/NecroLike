using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
    public class AIWander : AIBehaviour
    {
        private void OnEnable()
        {
            NavMesh.SamplePosition(Random.insideUnitSphere * 5 + entity.transform.position, out NavMeshHit hit, 5, NavMesh.AllAreas);
            entity.agent.SetDestination(hit.position);
            entity.anim.SetTrigger("Walk");
        }

        private void FixedUpdate()
        {
            if (entity.agent.remainingDistance <= entity.agent.stoppingDistance)
                finished.Invoke();
        }
    }
}

