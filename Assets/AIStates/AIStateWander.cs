using UnityEngine;
using UnityEngine.AI;

namespace AICreatures
{
    public class AIStateWander : AIState
    {
        float timeUntilNextMove;
        bool arrived;
        public override void Enter()
        {
        }

        public override void Update()
        {
            FireStateFinished();
            if(arrived)
            {
                timeUntilNextMove -= Time.deltaTime;
                if (timeUntilNextMove <= 0)
                {
                    arrived = false;
                    main.anim.SetTrigger("Walk");
                    NavMesh.SamplePosition(Random.insideUnitSphere * 5 + main.transform.position, out NavMeshHit hit, 5, NavMesh.AllAreas);
                    main.agent.SetDestination(hit.position);
                }
            }
            else if (main.agent.remainingDistance <= main.agent.stoppingDistance)
            {
                arrived = true;
                main.anim.SetTrigger("Idle");
                timeUntilNextMove = Random.Range(1, 5);
            }
        }
        public override void Exit()
        {
            main.agent.destination = main.transform.position;
        }
    }
}