using UnityEngine;
namespace AICreatures
{
    public abstract class AIState
    {
        protected AICreature main;
        private int id;

        public int GetID()
        {
            return id;
        }
        protected void FireStateFinished()
        {
            main.FinishedState(GetID());
        }
        public void Init(AICreature main)
        {
            AIManager.AIStateTypeDictionary.TryGetValue(GetType(), out AIManager.AIStateType type);
            id = (int)type;
            this.main = main;
        }

        public abstract void Enter();

        public abstract void Exit();

        public abstract void Update();

        public virtual void VisualUpdate(){ }
    }
}