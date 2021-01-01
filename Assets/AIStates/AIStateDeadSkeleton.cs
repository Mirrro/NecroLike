using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICreatures
{
    public class AIStateDeadSkeleton : AIState
    {
        public override void Enter()
        {
            Game.GetSkeletonDeathAnimInstance(main.transform.position);
            FireStateFinished();
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            
        }
    }
}