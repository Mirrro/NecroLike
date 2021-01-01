using UnityEngine;

namespace AICreatures
{
    public class AIStateIdle : AIState
    {
        public override void Enter()
        {
            main.anim.SetTrigger("Idle");
            FireStateFinished();
        }

        public override void Exit()
        {

        }

        public override void Update()
        {
        }
    }
}