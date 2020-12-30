using UnityEngine;

namespace AICreatures
{
    public class AIStateIdle : AIState
    {
        private float timeLeft;
        public static int ID = 0;
        public override int GetID()
        {
            return ID;
        }
        public override void Enter()
        {
            timeLeft = Random.Range(1, 5);
        }

        public override void Exit()
        {

        }

        public override void Update()
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
                main.FinishedState(ID);
        }
    }
}