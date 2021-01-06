using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
    public class AIIdle : AIBehaviour
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
            if (timer == 0)
                return;
            passedTime += Time.deltaTime;
            if (passedTime >= timer)
                finished.Invoke();
        }
    }
}

