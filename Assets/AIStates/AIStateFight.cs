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
        float attackPreparation;
        public override void Enter()
        {
            attackPreparation = 1 / main.attackspeed;
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            if (Vector3.Distance(main.GetTarget().transform.position, main.transform.position) <= main.range)
            {
                if (attackPreparation <= 0)
                    Attack();
                else
                    attackPreparation -= Time.deltaTime;
            }
            else
                main.FinishedState(ID);
        }

        private void Attack()
        {
            attackPreparation = 1 / main.attackspeed;
            if (Vector3.Distance(main.GetTarget().transform.position, main.transform.position) <= main.range)
                main.GetTarget().GetHit(main);
        }
    }
}