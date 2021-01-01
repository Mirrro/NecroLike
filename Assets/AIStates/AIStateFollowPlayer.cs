using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICreatures
{
    public class AIStateFollowPlayer : AIState
    {
        Transform player;

        public override void Enter()
        {
            player = Game.GetPlayer().transform;
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            main.agent.destination = player.transform.position;
            if (main.agent.remainingDistance < 2)
                FireStateFinished();
        }
    }
}
