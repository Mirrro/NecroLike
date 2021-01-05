using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AICreatures
{
    [RequireComponent(typeof(AIEntity))]
    public class AIBehaviour : MonoBehaviour
    {
        protected AIEntity entity;
        public UnityEvent finished;
        private void Awake()
        {
            entity = GetComponent<AIEntity>();
            enabled = false;
        }

        public void SetBehaviour()
        {
            entity.ChangeBehaviour(this);
        }

    }
}