using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AICreatures
{
    [RequireComponent(typeof(AIEntity))]
    public class DefaultBehaviour : AIBehaviour
    {
        public override void InitStates()
        {
            actions = new AIAction[] { new ActionWait(this), new ActionWalk(this) };
        }

        protected override void Check()
        {
            throw new System.NotImplementedException();
        }
    }
    
}