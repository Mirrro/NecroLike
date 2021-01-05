using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
    public class AIRally : AIBehaviour
    {
        private void OnEnable()
        {
            entity.agent.SetDestination(Game.RallyPoint);
        }

        private void FixedUpdate()
        {
            if (entity.agent.remainingDistance <= 2)
            {
                entity.forced = false;
                finished.Invoke();
            }
        }
    }
}