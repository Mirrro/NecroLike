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
        bool idling;
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
                if (!idling)
                {
                    entity.anim.SetTrigger("Idle");
                    idling = true;
                }
                timeUntilNextMove -= Time.deltaTime;
                if (timeUntilNextMove <= 0)
                    SetDestination();
            }
        }
        private void SetDestination()
        {
            idling = false;
            entity.anim.SetTrigger("Patrol");
            timeUntilNextMove = waitTime;
            entity.agent.SetDestination(waypoints[currentWaypoint]);
            if (++currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;
        }
    }
}