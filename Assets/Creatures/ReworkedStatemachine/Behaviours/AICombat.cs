using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICreatures
{
    public class AICombat : AIBehaviour
    {
        public bool fighter;
        AIAction combatAction;
        AICombat()
        {
            if (fighter)
                actions = new AIAction[] { new ActionWait(this), new ActionWalk(this), new ActionAttack(this) };
            else
                actions = new AIAction[] { new ActionFlee(this) };
            
        }
        private void OnEnable()
        {
            currentAction = 0;
        }
        
        private void Fight()
        {
            switch (currentAction)
            {
                case 0:
                    {
                        if (entity.GetNearestEnemy() != null)
                            ChangeAction(2);
                        else
                            ChangeAction(1);

                        break;
                    }
                case 1:
                    {
                        if (entity.GetNearestEnemy() != null)
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
            if(fighter)
                Fight();
        }
    }
}
