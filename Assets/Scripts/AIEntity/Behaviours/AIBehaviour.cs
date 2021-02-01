using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AIUnits
{
    [RequireComponent(typeof(AIEntity))]
    public abstract class AIBehaviour : MonoBehaviour
    {
        [HideInInspector] public AIEntity entity;
        protected AIAction[] actions;
        protected int currentAction;
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
            OnRun();
        }
        public virtual void OnRun()
        { }

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