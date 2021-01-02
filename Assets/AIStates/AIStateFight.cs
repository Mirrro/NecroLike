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
        ITargetable target;
        public override void Enter()
        {
            target = main.GetTarget();

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
                    if (main.IsInRange(target.GetPosition()))
                        target.GetHit(main.damage);
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
            if (target != null && target.IsAlive())
                main.transform.rotation = Quaternion.LookRotation(target.GetPosition() - main.transform.position);
        }
        public override void Update()
        {
            if (target != null && target.IsAlive())
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
                else if (!main.IsInRange(target.GetPosition()))
                {
                    main.anim.SetTrigger("Chase");
                    main.agent.destination = target.GetPosition();
                }
                else
                    Attack();
            }
            else
                target = main.GetTarget();
        }

        public override void Exit()
        {
        }
    }
}