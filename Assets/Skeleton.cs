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
            InitState(new AIStateChase());
            InitState(new AIStateFight());
            InitState(new AIStateIdle());

            ChangeState(AIStateIdle.ID);
        }

        public override void FinishedState(int state)
        {
            print(state);
            if (state == AIStateChase.ID)
                ChangeState(AIStateFight.ID);

            if (state == AIStateFight.ID)
                ChangeState(AIStateChase.ID);
        }

        public override void TargetFound()
        {
            ChangeState(AIStateChase.ID);
        }
    }
}