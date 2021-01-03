using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICreatures
{
    public class AIStateDeadHuman : AIState
    {
        private float timeLeft;
        public override void Enter()
        {
            timeLeft = 1;
            main.anim.SetTrigger("Death");
            main.agent.enabled = false;
        }

        public override void Exit()
        {
            Game.Instantiate(Game.GetSkeletonPrefab(), main.transform.position, main.transform.rotation, Game.GetMOBS());
        }


        public override void Update()
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
                FireStateFinished();
            
        }

        public override void VisualUpdate()
        {
        }
    }
}