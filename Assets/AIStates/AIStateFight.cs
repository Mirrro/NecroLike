using UnityEngine;

namespace AICreatures
{
    public class AIStateFight : AIState
    {
        int attackState; // 0 = anim | 1 = prehit | 2 = posthit | 3 = cooldown
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

        private void Attack()
        {
            attackTime += Time.deltaTime;
            if (attackState == 0)
            {
                if (main.anim != null)
                    main.anim.SetTrigger("Attack");
                attackState = 1;
                main.agent.destination = main.transform.position;
            }
            else if (attackState == 1)
            {
                if (attackTime >= attackPrepareTime)
                {
                    attackState = 2;
                    if (main.IsInRange(main.GetTarget().GetPosition()))
                        main.GetTarget().GetHit(main.damage);
                }
            }
            else if (attackState == 2)
            {
                if (attackTime >= attackAnimationLength)
                    attackState = 3;

                if (main.anim != null)
                    main.anim.SetTrigger("Idle");
            }

        }
        public override void VisualUpdate()
        {
            if (main.GetTarget() != null && main.GetTarget().IsAlive())
                main.transform.rotation = Quaternion.LookRotation(main.GetTarget().GetPosition() - main.transform.position);
        }
        public override void Update()
        {
            if (main.GetTarget() != null && main.GetTarget().IsAlive())
            {

                if (attackState == 3)
                {
                    attackTime += Time.deltaTime;
                    if (attackTime >= attackAnimationLength + attackCooldown)
                    {
                        attackState = 0;
                        attackTime = 0;
                    }
                }
                else if (!main.IsInRange(main.GetTarget().GetPosition()))
                {
                    main.anim.SetTrigger("Chase");
                    main.agent.destination = main.GetTarget().GetPosition();
                }
                else
                    Attack();
            }
        }

        public override void Exit()
        {
        }
    }
}