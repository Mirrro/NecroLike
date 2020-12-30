using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICreatures
{ 
    public class Skeleton : AICreature
    {
        public float tetherDistance;
        public bool forceFollowPlayer;

        void Awake()
        {
            InitState(new AIStateFollowPlayer());
            InitState(new AIStateIdle());
            InitState(new AIStateChase());
            InitState(new AIStateFight());

            ChangeState(AIStateFollowPlayer.ID);
        }

        public override void FinishedState(int state)
        {
            if (forceFollowPlayer)
                ChangeState(AIStateFollowPlayer.ID);
            else if(GetTarget() != null)
            {
                if (state != AIStateChase.ID)
                    ChangeState(AIStateChase.ID);
                else
                    ChangeState(AIStateFight.ID);
            }
            else if (state == AIStateFollowPlayer.ID)
                ChangeState(AIStateIdle.ID);
        }
        public override void Death()
        {
            base.Death();
            Destroy(gameObject);
        }

        public override void TargetFound(AICreature target)
        {
            base.TargetFound(target);
            if (!forceFollowPlayer && currentState.GetID() != AIStateChase.ID && currentState.GetID() != AIStateFight.ID)
                ChangeState(AIStateChase.ID);
        }
    }
}