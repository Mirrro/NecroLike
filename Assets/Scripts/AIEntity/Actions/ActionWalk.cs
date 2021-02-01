using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIUnits
{
    public class ActionWalk : AIAction
    {
        public ActionWalk(AIBehaviour main) : base(main) { }
        public override bool Run()
        {
            return main.entity.agent.remainingDistance <= 2;
        }
        public override void Init()
        {
        }
    }
}
