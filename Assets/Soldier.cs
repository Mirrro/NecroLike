using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICreatures
{
    public class Soldier : AICreature
    {
        private void Awake()
        {
            InitState(new AIStateIdle());
            InitState(new AIStateWander());
            InitState(new AIStateFight());
            InitState(new AIStateChase());

            ChangeState(AIStateIdle.ID);
        }
        public override void FinishedState(int state)
        {
            if (state == AIStateWander.ID)
                ChangeState(AIStateIdle.ID);

            if (state == AIStateIdle.ID)
                ChangeState(AIStateWander.ID);

            if (state == AIStateChase.ID)
                ChangeState(AIStateFight.ID);

            if (state == AIStateFight.ID)
            {
                if(GetTarget() == null)
                    ChangeState(AIStateIdle.ID);
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

        public override void Death()
        {
            base.Death();
            Destroy(gameObject);
            Instantiate(Game.GetSkellyFab(), transform.position, transform.rotation);
        }
    }
}