using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace AICreatures
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Collider))]
    public class AIEntity : MonoBehaviour
    {
        [Header("Combat Settings")]
        public Game.Team team;
        public Game.Team enemyTeam;
        public Stats stats;

        [Header("Combat Events")]
        public UnityEvent HitEvent = new UnityEvent();
        public UnityEvent DeathEvent = new UnityEvent();
        public UnityEvent EnemyFoundEvent = new UnityEvent();

        [HideInInspector]
        public int ID;
        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public NavMeshAgent agent;

        private void Start()
        {
            Game.RegisterCreature((int)team);
            anim = GetComponentInChildren<Animator>();
            agent = GetComponent<NavMeshAgent>();
            currentBehaviour.enabled = true;
        }

        private void FixedUpdate()
        {
            if((nearestEnemy = FindNearestVisibleEnemy()) != null)
                EnemyFoundEvent.Invoke();
        }

        #region Combat
        private AIEntity nearestEnemy;
        private AIEntity FindNearestVisibleEnemy()
        {
            int layerMask = 1 << (6 + ((int)enemyTeam));
            Collider[] hitColliders = Physics.OverlapSphere(GetPosition(), stats.vision, layerMask);

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
            DeathEvent.Invoke();
        }
        public void GetHit(int damage)
        {
            stats.health -= damage;
            HitEvent.Invoke();
        }
        public Vector3 GetPosition()
        {
            return transform.position;
        }
        public bool IsAlive()
        {
            return this != null && stats.health > 0;
        }
        #endregion

        #region Statemachine
        public AIBehaviour currentBehaviour;
        public void ChangeBehaviour(AIBehaviour newBehaviour)
        {
            if (currentBehaviour == newBehaviour)
                return;
            currentBehaviour.enabled = false;
            currentBehaviour = newBehaviour;
            currentBehaviour.enabled = true;
        }
        #endregion
        
    }

}

public struct Stats
{
    public int health;
    public float vision;

}