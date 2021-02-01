using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace AIUnits
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(DefaultBehaviour))]
    [RequireComponent(typeof(AICombat))]
    public class AIEntity : MonoBehaviour
    {
        public UnityEvent<AIEntity> killEvent = new UnityEvent<AIEntity>();
        public UnityEvent<AIEntity> hitEvent = new UnityEvent<AIEntity>();
        public UnityEvent<AIEntity> deathEvent = new UnityEvent<AIEntity>();

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
            anim = GetComponentInChildren<Animator>();
            agent = GetComponent<NavMeshAgent>();
            combatBehaviour = GetComponent<AICombat>();
            defaultBehaviour = GetComponent<DefaultBehaviour>();       
        }

        private void Start()
        {
            Game.level.RegisterUnit(this);

            if (Game.level.currentState == Level.State.Fighting)
                GameStart();
            else
            {
                enabled = false;
                agent.enabled = false;
            }
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
            nearestEnemy = FindNearestEnemy();
            if (!forced && !dead)
            {
                combatBehaviour.enabled = nearestEnemy != null;
                defaultBehaviour.enabled = nearestEnemy == null;
            }
        }

        #region Combat
        public static bool IsAlive(AIEntity entity)
        {
            return entity != null && entity.stats.lostHealth >= entity.stats.health;
        }

        private AIEntity nearestEnemy;
        private AIEntity FindNearestEnemy()
        {
            AIEntity[] enemies = Game.level.GetEnemies(enemyTeam);
            if (enemies.Length == 0)
                return null;
            AIEntity closestEnemy = enemies[0];
            foreach (AIEntity enemy in enemies)
            {
                if (Vector3.Distance(enemy.transform.position, transform.position) < Vector3.Distance(closestEnemy.transform.position, transform.position))
                    closestEnemy = enemy;
            }
            return closestEnemy;            
        }
        public AIEntity GetNearestEnemy()
        {
            return nearestEnemy;
        }
        public void Death()
        {
            deathEvent.Invoke(this);
            enabled = false;
            agent.enabled = false;
            combatBehaviour.enabled = false;
            defaultBehaviour.enabled = false;
            dead = true;
            gameObject.layer = 0;
            anim.SetTrigger("Death");
        }
        public bool GetHit(int damage)
        {
            stats.lostHealth += damage;
            hitEvent.Invoke(this);
            if (stats.lostHealth>=stats.health && !dead)
            {
                Death();
                return true;
            }
            return false;
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

        public void OnLevelStateBegin(Level.State state)
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
    public int lostHealth;
    public int damage;
    public float range;
    public float speed;
}