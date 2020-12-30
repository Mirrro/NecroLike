namespace AICreatures
{
    public abstract class AIState
    {
        protected AICreature main;

        public abstract int GetID();

        public void Init(AICreature main)
        {
            this.main = main;
        }

        public abstract void Enter();

        public abstract void Exit();

        public abstract void Update();
    }
}