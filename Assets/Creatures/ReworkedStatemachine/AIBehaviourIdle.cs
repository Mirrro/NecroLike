using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
    public class AIBehaviourIdle : AIBehaviour
    {
        public float timer;
        private float passedTime;
        private void OnEnable()
        {
            passedTime = 0;
            entity.anim.SetTrigger("Idle");
        }

        private void FixedUpdate()
        {
            passedTime += Time.deltaTime;
            if (passedTime >= timer)
                finished.Invoke();
        }
    }
}

