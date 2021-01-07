using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
    public class AIWander : DefaultBehaviour
    {
        public float wanderingDistance;
        public Transform wanderingCenter;
        public AIWander()
        {
            actions = new AIAction[] { new ActionWait(this), new ActionWalk(this) };
        }
        
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
                    entity.anim.SetTrigger("Idle");
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

