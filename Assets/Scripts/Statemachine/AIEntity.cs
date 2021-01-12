using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace AICreatures
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(DefaultBehaviour))]
    [RequireComponent(typeof(AICombat))]
    public class AIEntity : MonoBehaviour, ILevelStateListener
    {
        private bool dead;
        [Header("Combat Settings")]
        public Game.Team team;
        public Game.Team enemyTeam;
        public Stats stats;
        
        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public NavMeshAgent agent;

        private AICombat combatBehaviour;
        private DefaultBehaviour defaultBehaviour;

        private void Awake()
        {
            Level.InitStateListener(this);
            anim = GetComponentInChildren<Animator>();
            agent = GetComponent<NavMeshAgent>();
            enabled = false;
            agent.enabled = false;
            anim.enabled = false;
            combatBehaviour = GetComponent<AICombat>();
            defaultBehaviour = GetComponent<DefaultBehaviour>();
        }

        private void Start()
        {
            Game.CreatureSpawn(team);
        }

        public void GameStart()
        {            
            anim.SetTrigger("Spawn");
            enabled = true;
            agent.enabled = true;
            anim.enabled = true;
        }


        private void FixedUpdate()
        {
            nearestEnemy = FindNearestVisibleEnemy();
            if(!forced)
            {
                combatBehaviour.enabled = nearestEnemy != null;
                defaultBehaviour.enabled = nearestEnemy == null;
            }
        }

        #region Combat
        public static bool IsAlive(AIEntity entity)
        {
            return entity != null && entity.stats.health > 0;
        }

        private AIEntity nearestEnemy;
        private AIEntity FindNearestVisibleEnemy()
        {
            int layerMask = 1 << (6 + ((int)enemyTeam));
            Collider[] hitColliders = Physics.OverlapSphere(GetPosition(), 7, layerMask);

            layerMask = ~layerMask;
            Collider closestCollider = null;
            foreach (var hitCollider in hitColliders)
            {
                if (!Physics.Raycast(transform.position, hitCollider.transform.position - transform.position, out RaycastHit hit, Vector3.Distance(transform.position, hitCollider.transform.position), layerMask))
                {
                    if (closestCollider == null || Vector3.Distance(transform.position, hitCollider.transform.position) < Vector3.Distance(transform.position, closestCollider.transform.position))
                        closestCollider = hitCollider;
                }
            }
            if (closestCollider == null)
                return null;
            return closestCollider.GetComponent<AIEntity>();
            
        }
        public AIEntity GetNearestEnemy()
        {
            return nearestEnemy;
        }
        public void Death()
        {
            if (dead == true)
                return;
            enabled = false;
            agent.enabled = false;
            combatBehaviour.enabled = false;
            defaultBehaviour.enabled = false;
            dead = true;
            gameObject.layer = 0;
            Game.CreatureDeath(team);
            anim.SetTrigger("Death");
        }
        public void GetHit(int damage)
        {
            stats.health -= damage;
            if (stats.health <= 0)
                Death();
        }
        public Vector3 GetPosition()
        {
            return transform.position;
        }
   
        public bool IsInRange(Vector3 position)
        {
            return (Vector3.Distance(position, transform.position) <= stats.range);
        }
        #endregion

        #region Statemachine
        [HideInInspector]
        public bool forced;        
        public void ForceBehaviour(AIBehaviour behaviour)
        {
            combatBehaviour.enabled = false;
            defaultBehaviour.enabled = false;
            behaviour.enabled = true;
            forced = true;
        }
        #endregion

        public void OnStateEnd(Level.State state)
        {
        }

        public void OnStateBegin(Level.State state)
        {
            if (state == Level.State.Fighting)
                GameStart();
        }
    }

}
[System.Serializable]
public struct Stats
{
    public int health;
    public int damage;
    public float range;
    public float speed;
}