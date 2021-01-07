using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AICreatures
{
    [RequireComponent(typeof(AIEntity))]
    public abstract class DefaultBehaviour : AIBehaviour
    {
        public DefaultBehaviour()
        {
            actions = new AIAction[] { new ActionWait(this), new ActionWalk(this) };
        }
    }
    
}