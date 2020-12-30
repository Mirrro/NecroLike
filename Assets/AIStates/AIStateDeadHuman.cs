using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICreatures
{
    public class AIStateDeadHuman : AIState
    {
        private float timeLeft;
        public static int ID = 7;
        Player player;
        public override int GetID()
        {
            return ID;
        }
        public override void Enter()
        {
            player = Game.GetPlayer();
            timeLeft = player.reviveTime;
        }

        public override void Exit()
        {

        }

        public override void Update()
        {
            if (timeLeft <= 0)
                main.FinishedState(ID);
            else if (Vector3.Distance(player.transform.position, main.transform.position) <= player.reviveRange)
                timeLeft -= Time.deltaTime;
            else
                timeLeft = player.reviveTime;
        }
    }
}