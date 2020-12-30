using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
    public class Human : AICreature
    {
        private void Start()
        {
            InitState(new AIStateIdle());
            InitState(new AIStateWander());
            InitState(new AIStateFlee());
            InitState(new AIStateDeadHuman());

            if (IsAlive())
                ChangeState(AIStateIdle.ID);
            else
                ChangeState(AIStateDeadHuman.ID);

        }
        public override void FinishedState(int state)
        {
            if (state == AIStateWander.ID)
                ChangeState(AIStateIdle.ID);

            if (state == AIStateIdle.ID)
                ChangeState(AIStateWander.ID);

            if (state == AIStateFlee.ID)
                ChangeState(AIStateIdle.ID);

            if (state == AIStateDeadHuman.ID)
            {
                Destroy(gameObject);
                Game.GetPlayer().SpawnSkeleton(this);
            }

        }
        public override void TargetFound(AICreature target)
        {
            base.TargetFound(target);
            if (IsAlive() && currentState.GetID()!= AIStateFlee.ID)
                ChangeState(AIStateFlee.ID);
        }
        public override void Death()
        {
            base.Death();
            GetComponent<Renderer>().material = Game.GetDeadMat();
            ChangeState(AIStateDeadHuman.ID);
        }

    }
    
}
