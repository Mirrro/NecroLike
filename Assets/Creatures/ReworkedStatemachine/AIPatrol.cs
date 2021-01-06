using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
    public class AIPatrol : AIBehaviour
    {
        public Vector3[] waypoints;
        public float waitTime;
        int currentWaypoint;
        float timeUntilNextMove;
        private void OnEnable()
        {
            currentWaypoint = 0;
            SetDestination();
        }

        private Transform startTransform;
        private void FixedUpdate()
        {
            if (entity.agent.remainingDistance <= entity.agent.stoppingDistance)
            {
                if (timeUntilNextMove <= 0)
                {
                    timeUntilNextMove = waitTime;
                    entity.anim.SetTrigger("Idle");
                }

                timeUntilNextMove -= Time.deltaTime;

                if (timeUntilNextMove <= 0)
                    SetDestination();
            }
        }
        private void SetDestination()
        {
            if (++currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;
            entity.anim.SetTrigger("Patrol");
            entity.agent.SetDestination(waypoints[currentWaypoint]);
        }
    }
}