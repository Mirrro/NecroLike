using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
    public class AIDeath : AIBehaviour
    {
        public GameObject skeleton;
        public float timeUntilDestruction;
        private float timeLeft;
        private void OnEnable()
        {
            timeLeft = timeUntilDestruction;
            entity.anim.SetTrigger("Death");
            entity.agent.enabled = false;
        }

        private void FixedUpdate()
        {
            if (timeLeft <= 0)
            {
                finished.Invoke();
                Instantiate(skeleton, entity.transform.position, entity.transform.rotation, Level.Mobs);
            }
            else
                timeLeft -= Time.deltaTime;
        }
    }
}