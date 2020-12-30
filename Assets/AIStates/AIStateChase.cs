namespace AICreatures
{
    public class AIStateChase : AIState
    {
        public static int ID = 3;
        public override int GetID()
        {
            return ID;
        }
        AICreature target;
        
        public override void Enter()
        {
            target = main.GetTarget();
            if(target!=null)
                main.agent.destination = target.transform.position;
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            if (!main.IsValidTarget(target) || main.IsInRange(target))
                main.FinishedState(ID);

            else
                main.agent.destination = target.transform.position;
        }
    }
}