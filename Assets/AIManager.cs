using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AICreatures
{
    public class AIManager : MonoBehaviour
    {

        public static Dictionary<Type, AIStateType> AIStateTypeDictionary = new Dictionary<Type, AIStateType>
        {
            {typeof(AIStateSpawning),AIStateType.Spawning},

            {typeof(AIStateFollowPlayer),AIStateType.FollowPlayer},
            {typeof(AIStateIdle),AIStateType.Idle},
            {typeof(AIStateWander),AIStateType.Wander},
            {typeof(AIStatePatrol),AIStateType.Patrol},
            
            {typeof(AIStateFlee),AIStateType.Flee},
            {typeof(AIStateFight),AIStateType.Fight},

            {typeof(AIStateDeadHuman),AIStateType.DeadHuman},
            {typeof(AIStateDeadSkeleton),AIStateType.DeadSkeleton},

        };

        public static Type[] AIStateTypes = new Type[]
        {
            typeof(AIStateSpawning),

            typeof(AIStateFollowPlayer),
            typeof(AIStateIdle),
            typeof(AIStateWander),
            typeof(AIStatePatrol),

            typeof(AIStateFlee),
            typeof(AIStateFight),

            typeof(AIStateDeadHuman),
            typeof(AIStateDeadSkeleton)
        };

        public enum AIStateType
        {
            Spawning,

            FollowPlayer,
            Idle,
            Wander,
            Patrol,

            Flee,
            Fight,

            DeadHuman,
            DeadSkeleton
        }

        public static AIState ActivateState(AIStateType type)
        {
           return (AIState)Activator.CreateInstance(AIStateTypes[(int)type]);
        }
    }
}