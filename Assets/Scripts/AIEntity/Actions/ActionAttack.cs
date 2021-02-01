using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIUnits
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
            if (timeLeft <= 0)
            { 
                AIEntity enemy = main.entity.GetNearestEnemy();
                if (enemy != null && main.entity.IsInRange(enemy.GetPosition()))
                    if (enemy.GetHit(main.entity.stats.damage))
                        main.entity.killEvent.Invoke(enemy);                
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