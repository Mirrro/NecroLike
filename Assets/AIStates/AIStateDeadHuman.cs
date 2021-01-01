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
        LineRenderer line;
        public override int GetID()
        {
            return ID;
        }
        public override void Enter()
        {
            player = Game.GetPlayer();
            line = Game.GetLinePrefabInstance().GetComponent<LineRenderer>();
            line.SetPosition(0, main.transform.position);
            timeLeft = player.reviveTime;
        }

        public override void Exit()
        {
            Game.Destroy(line.gameObject);
        }

        public override void Update()
        {
            if (timeLeft <= 0)
                main.FinishedState(ID);
            else if (Vector3.Distance(player.transform.position, main.transform.position) <= player.reviveRange)
            {
                line.SetPosition(1, player.transform.position);
                line.enabled = true;
                timeLeft -= Time.deltaTime;
            }
            else
            {
                timeLeft = player.reviveTime;
                line.enabled = false;
            }
        }
    }
}