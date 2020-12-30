using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICreatures
{ 
    public class Skeleton : AICreature
    {
        public float tetherDistance;

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
            if(state == AIStateFollowPlayer.ID && GetTarget() == null)
                ChangeState(AIStateIdle.ID);
            else if (state == AIStateIdle.ID && GetTarget() == null)
                ChangeState(AIStateFollowPlayer.ID);
            else if (state == AIStateChase.ID)
                ChangeState(AIStateFight.ID);

            else if (state == AIStateFight.ID)
            {
                if (Vector3.Distance(transform.position, Game.GetPlayer().position) > tetherDistance)
                    ChangeState(AIStateFollowPlayer.ID);
                else
                    ChangeState(AIStateChase.ID);
            }
        }

        public override void TargetFound(AICreature target)
        {
            base.TargetFound(target);
            if (currentState.GetID() != AIStateChase.ID && currentState.GetID() != AIStateFight.ID)
                ChangeState(AIStateChase.ID);
        }
    }
}