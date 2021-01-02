using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICreatures
{
    public class AIStateDeadHuman : AIState
    {
        private float timeLeft;
        Player player;
        LineRenderer line;
        public override void Enter()
        {
            player = Game.GetPlayer();
            line = Game.GetLinePrefabInstance().GetComponent<LineRenderer>();
            line.SetPosition(0, main.transform.position);
            timeLeft = player.reviveTime;
            main.anim.SetTrigger("Death");
            main.agent.enabled = false;
        }

        public override void Exit()
        {
            Game.Destroy(line.gameObject);
            player.SpawnSkeleton(main);
        }


        public override void Update()
        {
            if (timeLeft <= 0)
            {
                FireStateFinished();
            }
            else if (Vector3.Distance(player.transform.position, main.transform.position) <= player.reviveRange)
            {
                line.enabled = true;
                timeLeft -= Time.deltaTime;
            }
            else
            {
                timeLeft = player.reviveTime;
                line.enabled = false;
            }
        }

        public override void VisualUpdate()
        {
            line.SetPosition(1, player.transform.position);
        }
    }
}