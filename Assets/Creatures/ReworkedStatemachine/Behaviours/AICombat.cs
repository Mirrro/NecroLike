using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICreatures
{
    public class AICombat : AIBehaviour
    {
        AIAction combatAction;
        public override void InitStates()
        {
            if (entity.stats.damage>0)
                actions = new AIAction[] { new ActionWait(this), new ActionWalk(this), new ActionAttack(this) };
            else
                actions = new AIAction[] { new ActionFlee(this) };
            
        }
        private void OnEnable()
        {
            ChangeAction(0);
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
                        {
                            entity.agent.SetDestination(entity.GetNearestEnemy().GetPosition());
                            ChangeAction(1);
                        }

                        break;
                    }
                case 1:
                    {
                        if (entity.IsInRange(entity.GetNearestEnemy().GetPosition()))
                            ChangeAction(2);
                        else
                        {
                            entity.agent.SetDestination(entity.GetNearestEnemy().GetPosition());
                            ChangeAction(1);
                        }
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
