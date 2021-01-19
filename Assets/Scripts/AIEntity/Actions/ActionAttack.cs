using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICreatures
{
    public class ActionAttack : AIAction
    {
        private float timeLeft;
        public ActionAttack(AIBehaviour main, float time) : base(main)
        {
            timeLeft = time;        
        }
        public override bool Run()
        {
            if(timeLeft<= 0)
            {
                if (main.entity.GetNearestEnemy() != null)
                    if (main.entity.GetNearestEnemy().GetHit(main.entity.stats.damage))
                        main.entity.killEvent.Invoke(main.entity);
                
                return true;
            }

            timeLeft -= Time.deltaTime;
            return false;
        }
        public override void Init()
        {
            timeLeft = 10 / main.entity.stats.speed;
            main.entity.anim.SetTrigger("Attack");
        }
    }
}