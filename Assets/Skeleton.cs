using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICreatures
{ 
    public class Skeleton : AICreature
    {
        // Start is called before the first frame update
        void Start()
        {
            InitState(new AIStateWander());
            InitState(new AIStateChase());
            InitState(new AIStateFight());

            ChangeState(AIStateWander.ID);
        }

        public override void FinishedState(int state)
        {
            if (GetTarget() == null)
                ChangeState(AIStateWander.ID);

            else if (state == AIStateChase.ID)
                ChangeState(AIStateFight.ID);

            else if (state == AIStateFight.ID)
                ChangeState(AIStateChase.ID);
        }

        public override void TargetFound(AICreature target)
        {
            base.TargetFound(target);
            if (currentState.GetID() != AIStateChase.ID && currentState.GetID() != AIStateFight.ID)
                ChangeState(AIStateChase.ID);
        }
    }
}