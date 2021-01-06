using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
    public class AIRally : AIBehaviour
    {
        
        public void Rally(Vector3 rallyPoint)
        {
            entity.agent.SetDestination(rallyPoint);
            ForceBehaviour();
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