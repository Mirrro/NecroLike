using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICreatures
{
    public class ActionWalk : AIAction
    {
        public ActionWalk(AIBehaviour main) : base(main) { }
        public override bool Run()
        {
            return main.entity.agent.remainingDistance <= main.entity.agent.stoppingDistance;
        }
        public override void Init()
        {
        }
    }
}
