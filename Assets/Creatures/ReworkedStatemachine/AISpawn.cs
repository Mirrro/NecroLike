using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
    public class AISpawn : AIBehaviour
    {
        public float spawnTime;
        private float timeLeft;
        private void OnEnable()
        {
            entity.anim.SetTrigger("Spawn");
            timeLeft = spawnTime;
        }
        public void FixedUpdate()
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
                finished.Invoke();
        }
    }
}

