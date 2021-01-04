using System.Collections.Generic;
using System;

namespace AICreatures
{
    public static class AIManager
    {
        public enum AIStateType
        {
            Spawning,

            Idle,
            Wander,
            Patrol,

            Flee,
            Fight,

            DeadHuman,
            DeadSkeleton,

            Rally
        }

        public static Dictionary<Type, AIStateType> AIStateTypeDictionary = new Dictionary<Type, AIStateType>
        {
            {typeof(AIStateSpawning),AIStateType.Spawning},
            
            {typeof(AIStateIdle),AIStateType.Idle},
            {typeof(AIStateWander),AIStateType.Wander},
            {typeof(AIStatePatrol),AIStateType.Patrol},
            
            {typeof(AIStateFlee),AIStateType.Flee},
            {typeof(AIStateFight),AIStateType.Fight},

            {typeof(AIStateDeadHuman),AIStateType.DeadHuman},
            {typeof(AIStateDeadSkeleton),AIStateType.DeadSkeleton},


            {typeof(AIStateRally),AIStateType.Rally}
        };

        public static Type[] AIStateTypes = new Type[]
        {
            typeof(AIStateSpawning),
            
            typeof(AIStateIdle),
            typeof(AIStateWander),
            typeof(AIStatePatrol),

            typeof(AIStateFlee),
            typeof(AIStateFight),

            typeof(AIStateDeadHuman),
            typeof(AIStateDeadSkeleton),
            
            typeof(AIStateRally)
        };

        public static T ActivateState<T>() where T: AIState
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
        public static AIState ActivateState(AIStateType type)
        {
           return (AIState)Activator.CreateInstance(AIStateTypes[(int)type]);
        }
    }
}