using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AIUnits
{
    public class AIPatrol : DefaultBehaviour
    {
        public Vector3[] waypoints;
        int currentWaypoint;
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