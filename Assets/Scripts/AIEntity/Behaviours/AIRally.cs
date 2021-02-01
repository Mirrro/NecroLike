using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AIUnits
{
    public class AIRally : AIBehaviour
    {
        public override void InitStates()
        {
            actions = new AIAction[] { new ActionWalk(this) };
        }

        public void Rally(Vector3 rallyPoint)
        {
            entity.agent.SetDestination(rallyPoint);
            entity.ForceBehaviour(this);
            ChangeAction(0);
        }

        protected override void Check()
        {
            entity.forced = false;
            enabled = false;
        }
    }
}