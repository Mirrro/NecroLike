using UnityEngine;

namespace AICreatures
{
    public class AIStateFight : AIState
    {

        int attackState; // 0 = prepare | 1 = hit | 2 = cooldown
        float attackTime;
        float attackPrepareTime;
        float attackCooldown;
        float attackAnimationLength;
        public override void Enter()
        {
            attackState = 0;
            attackTime = 0;

            attackAnimationLength = main.attackAnimationLength;
            attackPrepareTime = main.attackPrepareTime;
            attackCooldown = main.attackCooldown;


            if (main.anim != null)
                main.anim.SetTrigger("Attack");
        }

        private void Chase()
        {
            attackState = 0;
            attackTime = 0;
            main.agent.destination = main.GetTarget().transform.position;
        }

        private void Attack()
        {
            attackTime += Time.deltaTime;
            if (attackState == 0)
            {
                if (attackTime >= attackPrepareTime)
                {
                    attackState = 1;
                    if (main.IsInRange(main.GetTarget()))
                        main.GetTarget().GetHit(main);
                }
                
            }
            if (attackState == 1)
            {
                if (attackTime >= attackAnimationLength)
                    attackState = 2;
            }
            else if (attackState == 2)
            {
                if (attackTime > attackCooldown + attackAnimationLength)
                {
                    attackState = 0;
                    attackTime = 0;
                    if (main.anim != null)
                        main.anim.SetTrigger("Attack");
                }
                
            }
        }

        public override void Update()
        {
            if (main.IsInRange(main.GetTarget()))
                Attack();
            else
                Chase();
        }

        public override void Exit()
        {
        }
    }
}