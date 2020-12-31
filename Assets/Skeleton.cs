using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICreatures
{ 
    public class Skeleton : AICreature
    {
        public float tetherDistance;
        void Start()
        {
            InitState(new AIStateSpawning());
            InitState(new AIStateFollowPlayer());
            InitState(new AIStateChase());
            InitState(new AIStateFight());

            ChangeState(AIStateSpawning.ID);
        }

        private bool IsTooFarFromPlayer()
        {
            return Vector3.Distance(Game.GetPlayer().transform.position, transform.position) > tetherDistance;
        }

        public override void FinishedState(int state)
        {
            if (IsTooFarFromPlayer())
                ChangeState(AIStateFollowPlayer.ID);
            else if(IsValidTarget(GetTarget()))
            {
                if (state != AIStateChase.ID)
                    ChangeState(AIStateChase.ID);
                else
                    ChangeState(AIStateFight.ID);
            }
            else
                ChangeState(AIStateFollowPlayer.ID);
        }

        public override void Death()
        {
            base.Death();
            Destroy(gameObject);
        }

        public override void TargetFound(AICreature target)
        {
            base.TargetFound(target);
            if (!IsTooFarFromPlayer() && currentState.GetID() != AIStateSpawning.ID && currentState.GetID() != AIStateChase.ID && currentState.GetID() != AIStateFight.ID)
                ChangeState(AIStateChase.ID);
        }
    }
}