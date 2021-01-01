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
        AICreature target;
        public override void Enter()
        {
            target = main.GetTarget(); 

            if (target != null)
                main.agent.destination = target.transform.position;
            
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
            main.agent.destination = target.transform.position;
        }

        private void Attack()
        {
            attackTime += Time.deltaTime;
            if (attackState == 0)
            {
                if (attackTime >= attackPrepareTime)
                {
                    attackState = 1;
                    if (main.IsInRange(target))
                        target.GetHit(main);
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
            if (!main.IsValidTarget(target))
                FireStateFinished();
            else if (main.IsInRange(target))
                Attack();
            else
                Chase();
        }

        public override void Exit()
        {
        }
    }
}