using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
    public class AIFight : AIBehaviour
    {
        private enum AttackPhase {Chase, Start, Prehit, Posthit, Cooldown }
        private AttackPhase phase;

        private float timeToNextPhase;
        private bool isInRange;

        [Header("Attack Animation Properties")]
        public float preHitPhase;
        public float postHitPhase;
        public float cooldown;

        private void OnEnable()
        {
            timeToNextPhase = 0;
            phase = AttackPhase.Start;
            entity.anim.SetTrigger("Attack");
        }
        private void Update()
        {
            if (AIEntity.IsAlive(entity.GetNearestEnemy()))
                entity.transform.rotation = Quaternion.LookRotation(entity.GetNearestEnemy().GetPosition() - entity.transform.position);

        }
        private bool UpdateInRangeChanged()
        {
            return (isInRange == (isInRange = entity.IsInRange(entity.GetNearestEnemy().GetPosition())));
        }
        private void FixedUpdate()
        {
            if (!AIEntity.IsAlive(entity.GetNearestEnemy()))
            {
                finished.Invoke();
                return;
            }
            if(UpdateInRangeChanged())
                entity.anim.SetTrigger("Chase");
            if(isInRange || phase == AttackPhase.Cooldown)
            {
                if (timeToNextPhase <= 0)
                    FinishAttackPhase();
                else
                    timeToNextPhase -= Time.deltaTime;
            }
            else if (!isInRange)
                entity.agent.destination = entity.GetNearestEnemy().GetPosition();
        }
        private void FinishAttackPhase()
        {
            switch(phase)
            {
                case AttackPhase.Start:
                {
                    entity.agent.destination = entity.transform.position;
                    entity.anim.SetTrigger("Attack");
                    timeToNextPhase = preHitPhase;
                    phase = AttackPhase.Prehit;
                    break;
                }
                case AttackPhase.Prehit:
                {
                    timeToNextPhase = postHitPhase;
                    entity.GetNearestEnemy().GetHit(entity.stats.damage);
                    phase = AttackPhase.Posthit;
                    break;
                }
                case AttackPhase.Posthit:
                {
                    entity.anim.SetTrigger("Idle");
                    timeToNextPhase = cooldown;
                    phase = AttackPhase.Cooldown;
                    break;
                }
                case AttackPhase.Cooldown:
                {
                    phase = AttackPhase.Start;
                    break;
                }
                default: break;
            }
        }
    }
}