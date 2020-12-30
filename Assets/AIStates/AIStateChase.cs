namespace AICreatures
{
    public class AIStateChase : AIState
    {
        public static int ID = 3;
        public override int GetID()
        {
            return ID;
        }
        public override void Enter()
        {
            main.agent.destination = main.GetTarget().transform.position;
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            if (main.agent.remainingDistance <= main.range)
                main.FinishedState(ID);

            else
                main.agent.destination = main.GetTarget().transform.position;
        }
    }
}