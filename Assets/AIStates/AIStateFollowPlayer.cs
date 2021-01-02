using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
    public class AIStateFollowPlayer : AIState
    {
        Transform player;

        public override void Enter()
        {
            player = Game.GetPlayer().transform;

        }

        private void SetDestination()
        {
            NavMesh.SamplePosition(Random.insideUnitSphere * 5 + player.transform.position, out NavMeshHit hit, 5, NavMesh.AllAreas);
            main.agent.SetDestination(hit.position);
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            if (Vector3.Distance(player.position, main.agent.destination) > 5)
                SetDestination();
            else if (Vector3.Distance(player.position, main.transform.position) < 5)
                FireStateFinished();

        }
    }
}
