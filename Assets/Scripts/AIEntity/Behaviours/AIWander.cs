using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AIUnits
{
    public class AIWander : DefaultBehaviour
    {
        public float wanderingDistance;
        public Transform wanderingCenter;
        
        protected override void Check()
        {
            switch (currentAction)
            {
                case 0:
                {
                    NavMesh.SamplePosition(Random.insideUnitSphere * wanderingDistance + wanderingCenter.position, out NavMeshHit hit, wanderingDistance, NavMesh.AllAreas);
                    entity.agent.SetDestination(hit.position);
                    entity.anim.SetTrigger("Walk");
                    ChangeAction(1);
                    break;
                }
                case 1:
                {
                    ChangeAction(0);
                    break;
                }
                default:
                {
                    break;
                }
            }
        }
    }
}

