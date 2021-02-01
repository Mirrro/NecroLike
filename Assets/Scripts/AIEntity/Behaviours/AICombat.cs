using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIUnits
{
    public class AICombat : AIBehaviour
    {
        [SerializeField] private float cooldown;
        [SerializeField] private float attackTime;
        public override void InitStates()
        {
            actions = new AIAction[] 
            {
                new ActionWait(this, cooldown),
                new ActionWalk(this),
                new ActionAttack(this, attackTime),
                new ActionFlee(this)
            };
            
        }
        private void OnEnable()
        {
            ChangeAction(entity.stats.damage > 0?0:3);
        }

        public override void OnRun()
        {
            if(entity.stats.damage > 0)
                entity.agent.SetDestination(entity.GetNearestEnemy().GetPosition());

        }

        private void Fight()
        {
            switch (currentAction)
            {
                case 0:
                    {
                        if (entity.IsInRange(entity.GetNearestEnemy().GetPosition()))
                            ChangeAction(2);
                        else
                            ChangeAction(1);
                        break;
                    }
                case 1:
                    {
                        if (entity.IsInRange(entity.GetNearestEnemy().GetPosition()))
                            ChangeAction(2);
                        else
                            ChangeAction(1);
                        break;
                    }

                case 2:
                    {
                        ChangeAction(0);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        protected override void Check()
        {
            if(entity.stats.damage > 0)
                Fight();
        }
    }
}
