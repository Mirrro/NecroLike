using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AICreatures
{
    [RequireComponent(typeof(AIEntity))]
    public abstract class AIBehaviour : MonoBehaviour
    {
        [HideInInspector]
        public AIEntity entity;
        protected AIAction[] actions;
        public int currentAction;
        private void Awake()
        {
            entity = GetComponent<AIEntity>();
            enabled = false;
            InitStates();
        }
        public abstract void InitStates();
        private void FixedUpdate()
        {
            if (actions == null)
                return;
            if (actions[currentAction].Run())
                Check();
        }

        public void ChangeAction(int newAction)
        {
            if (actions == null)
                return;
            currentAction = newAction;
            actions[currentAction].Init();
        }

        protected abstract void Check();

    }
}