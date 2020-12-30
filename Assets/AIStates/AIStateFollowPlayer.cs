using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICreatures
{
    public class AIStateFollowPlayer : AIState
    {
        public static int ID = 5;
        public override int GetID()
        {
            return ID;
        }
        Transform player;

        public override void Enter()
        {
            player = Game.GetPlayer().transform;
            if (player != null)
                main.agent.destination = player.transform.position;
        }

        public override void Exit()
        {
            main.agent.destination = main.transform.position;
        }

        public override void Update()
        {
            if (main.agent.remainingDistance<=2)
                main.FinishedState(ID);
            else
                main.agent.destination = player.transform.position;
        }
    }
}
