using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICreatures
{
    public class AIStateDeadSkeleton : AIState
    {
        public override void Enter()
        {
            Game.Instantiate(Game.GetSkeletonDeathPrefab(),main.transform.position,Quaternion.identity);
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