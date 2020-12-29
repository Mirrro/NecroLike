using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICreatures
{ 
    public class Skeleton : AICreature
    {
        // Start is called before the first frame update
        void Start()
        {
            InitState(new AIStateChase());
            InitState(new AIStateIdle());

            ChangeState(AIStateIdle.ID);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Human>())
            {
                if (!target)
                {
                    target = other.GetComponent<Human>();
                    ChangeState(AIStateChase.ID);
                }
            }
        }
    }
}