using UnityEngine;

namespace AICreatures
{
    public class AIStateIdle : AIState
    {
        public override void Enter()
        {
            FireStateFinished();
            main.anim.SetTrigger("Idle");
        }

        public override void Exit()
        {

        }

        public override void Update()
        {
        }
    }
}