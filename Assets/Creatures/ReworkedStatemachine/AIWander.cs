using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
    public class AIWander : AIBehaviour
    {
        public float wanderingDistance;
        public Transform wanderingCenter;
        private void OnEnable()
        {
            NavMesh.SamplePosition(Random.insideUnitSphere * wanderingDistance + wanderingCenter.position, out NavMeshHit hit, wanderingDistance, NavMesh.AllAreas);
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

