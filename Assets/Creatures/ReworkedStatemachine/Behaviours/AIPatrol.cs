using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
    public class AIPatrol : DefaultBehaviour
    {
        public Vector3[] waypoints;
        int currentWaypoint;
        public AIPatrol()
        {
            actions = new AIAction[] { new ActionWait(this), new ActionWalk(this) };
        }
        protected override void Check()
        {
            switch (currentAction)
            {
                case 0:
                {
                    if (++currentWaypoint >= waypoints.Length)
                        currentWaypoint = 0;
                    entity.anim.SetTrigger("Patrol");
                    entity.agent.SetDestination(waypoints[currentWaypoint]);
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