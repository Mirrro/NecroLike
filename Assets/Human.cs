﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
    public class Human : AICreature
    {
        private void Awake()
        {
            InitState(new AIStateIdle());
            InitState(new AIStateWander());
            InitState(new AIStateFlee());

            if(IsAlive())
                ChangeState(AIStateIdle.ID);
        }
        public override void FinishedState(int state)
        {
            if (state == AIStateWander.ID)
                ChangeState(AIStateIdle.ID);

            if (state == AIStateIdle.ID)
                ChangeState(AIStateWander.ID);

            if (state == AIStateFlee.ID)
                ChangeState(AIStateIdle.ID);

        }
        public override void TargetFound(AICreature target)
        {
            base.TargetFound(target);
            if (currentState.GetID()!= AIStateFlee.ID)
                ChangeState(AIStateFlee.ID);
        }
        private void OnMouseDown()
        {
            if (!IsAlive())
            {
                Destroy(gameObject);
                Game.GetPlayer().SpawnSkeleton(this);
            }
        }

    }
    
}
