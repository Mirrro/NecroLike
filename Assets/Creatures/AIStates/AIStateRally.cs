using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
    public class AIStateRally : AIState
    {
        public override void Enter()
        {
            main.agent.SetDestination(Game.RallyPoint);
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            if (main.agent.remainingDistance <= 2)
                FireStateFinished();
        }
    }
}
