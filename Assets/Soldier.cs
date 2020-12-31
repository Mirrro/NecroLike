using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICreatures
{
    public class Soldier : AICreature
    {
        Vector3 startPosition;
        private void Start()
        {
            InitState(new AIStateIdle());
            InitState(new AIStateFight());
            InitState(new AIStateChase());

            ChangeState(AIStateIdle.ID);
            startPosition = transform.position;
        }
        public override void FinishedState(int state)
        {   
            if (IsValidTarget(GetTarget()))
            {
                if (state != AIStateChase.ID)
                    ChangeState(AIStateChase.ID);
                else
                    ChangeState(AIStateFight.ID);
            }
            else
            {
                ChangeState(AIStateIdle.ID);
                agent.SetDestination(startPosition);
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