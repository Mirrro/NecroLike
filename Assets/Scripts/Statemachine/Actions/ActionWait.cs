using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICreatures
{
    public class ActionWait : AIAction
    {
        public ActionWait(AIBehaviour main) : base(main) { }
        private float timeLeft;
        public override bool Run()
        {
            timeLeft -= Time.deltaTime;
            return timeLeft <= 0;
        }
        public override void Init()
        {
            main.entity.anim.SetTrigger("Idle");
            timeLeft = 10/main.entity.stats.speed;
        }
    }

}
