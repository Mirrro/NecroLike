using UnityEngine;

namespace AICreatures
{
    public class AIStateFight : AIState
    {
        public static int ID = 4;
        public override int GetID()
        {
            return ID;
        }


        int attackState; // 0 = prepare | 1 = hit | 2 = cooldown
        float attackTime;
        float attackPrepareTime;
        float attackCooldown;
        float attackAnimationLength;
        AICreature target;
        public override void Enter()
        {
            target = main.GetTarget();
            main.agent.SetDestination(main.transform.position);
            attackState = 0;
            attackTime = 0;

            attackAnimationLength = main.attackAnimationLength;
            attackPrepareTime = main.attackPrepareTime;
            attackCooldown = main.attackCooldown;


            if (main.anim != null)
                main.anim.SetTrigger("Attack");
        }

        public override void Update()
        {
            attackTime += Time.deltaTime;
            if (attackState == 0)
            {
                if (attackTime >= attackPrepareTime)
                {
                    attackState = 1;
                    main.GetTarget().GetHit(main);
                }

            }
            if (attackState == 1)
            { 
                if (attackTime >= attackAnimationLength)
                    attackState = 2;
            }
            else if(attackState == 2)
            {
                if (!main.IsValidTarget(target) || !main.IsInRange(target))
                    main.FinishedState(ID);
                else
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
        }

        public override void Exit()
        {
        }
    }
}