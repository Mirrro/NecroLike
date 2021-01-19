using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICreatures
{
    public abstract class AIAction
    {
        protected AIBehaviour main;
        public AIAction(AIBehaviour main)
        {
            this.main = main;
        }
        public abstract void Init();
        public abstract bool Run();
    }
}