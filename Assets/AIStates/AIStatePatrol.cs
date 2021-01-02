using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICreatures
{
    public class AIStatePatrol : AIState
    {
        GameObject[] waypoints;
        int currentWaypoint;
        float timeUntilNextMove;
        bool idling;
        public override void Enter()
        {
            waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
            currentWaypoint = 0;
            SetDestination();
        }

        private void SetDestination()
        {
            idling = false;
            main.anim.SetTrigger("Patrol");
            timeUntilNextMove = 1;
            main.agent.SetDestination(waypoints[currentWaypoint].transform.position);
            if (++currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;
        }

        public override void Update()
        {
            if(main.agent.remainingDistance<=main.agent.stoppingDistance)
            {
                if(!idling)
                {
                    main.anim.SetTrigger("Idle");
                    idling = true;
                }
                timeUntilNextMove -= Time.deltaTime;
                if(timeUntilNextMove<=0)
                    SetDestination();
            }
            
        }
        public override void Exit()
        {
          
        }
    }
}