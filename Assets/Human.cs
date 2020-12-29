using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
    public class Human : AICreature
    {
        private void Start()
        {
            InitState(new AIStateIdle());
            InitState(new AIStateWander());
            InitState(new AIStateFlee());

            ChangeState(AIStateIdle.ID);
        }
        public override void FinishedState(int state)
        {
            if (state == AIStateWander.ID)
                ChangeState(AIStateIdle.ID);

            if (state == AIStateIdle.ID)
                ChangeState(AIStateWander.ID);
        }


        public void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Skeleton>())
            {
                if (!target)
                {
                    target = other.GetComponent<Skeleton>();
                    ChangeState(AIStateFlee.ID);
                }
            }
        }

        private void OnMouseDown()
        {
            if (Game.infected)
                return;
            Game.infected = true;
            Instantiate(Game.instance.skellyFab, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
    
}
