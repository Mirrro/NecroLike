using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICreatures
{
    public class ActionAttack : AIAction
    {
        public ActionAttack(AIBehaviour main) : base(main) { }
        private float timeLeft;
        public override bool Run()
        {
            if(timeLeft<= 0)
            {
                if (main.entity.GetNearestEnemy()!= null)
                    main.entity.GetNearestEnemy().GetHit(main.entity.stats.damage);
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