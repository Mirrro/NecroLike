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
        [HideInInspector]
        public int currentAction;
        private void Awake()
        {
            entity = GetComponent<AIEntity>();
            enabled = false;
        }

        private void FixedUpdate()
        {
            if (actions[currentAction].Run())
                Check();
        }

        public void ChangeAction(int newAction)
        {
            currentAction = newAction;
            actions[currentAction].Init();
        }

        protected abstract void Check();

    }
}