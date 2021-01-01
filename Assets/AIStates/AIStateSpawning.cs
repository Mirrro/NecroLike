using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AICreatures
{
    public class AIStateSpawning : AIState
    {
        private float timeLeft;
        public override void Enter()
        {
            if (main.anim != null)
                main.anim.SetTrigger("Spawn");
            timeLeft = 1;
        }

        public override void Exit()
        {

        }

        public override void Update()
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
                FireStateFinished();
        }
    }
}
