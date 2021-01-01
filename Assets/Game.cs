using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AICreatures;

public class Game : MonoBehaviour
{
    public static Game instance;
    public GameObject skellyFab;
    public GameObject skeletonDeathAnim;
    public Material deadPerson;
    public GameObject linePrefab;
    public Player player;
    public Vector2 input;
    public AICreature[][] creatures =
    {
        new AICreature[50],
        new AICreature[50]
    }
    ;
    public AICreature[] humans = new AICreature[50];

    private void Awake()
    {
        instance = this;
    }

    public AICreature GetCreature(int team, int id)
    {
        return creatures[team][id];
    }

    public static int RegisterCreature(AICreature creature)
    {
        for (int i = 0; i < instance.creatures[creature.team].Length; i++)
        {
            if (instance.creatures[creature.team][i] == null)
            {
                instance.creatures[creature.team][i] = creature;
                return i;
            }
        }
        return -1;
    }

    public static void UnregisterCreature(AICreature creature)
    {
        instance.creatures[creature.team][creature.ID] = null;
    }

    public static Vector2 GetInput()
    {
        return instance.input;
    }

    public static void SetInput(Vector2 input)
    {
        instance.input = input;
    }
    public static Player GetPlayer()
    {
        return instance.player;
    }
    public static GameObject GetSkellyFab()
    {
        return instance.skellyFab;
    }
    public static Material GetDeadMat()
    {
        return instance.deadPerson;
    }
    public static GameObject GetSkeletonDeathAnimInstance(Vector3 position)
    {
        return Instantiate(instance.skeletonDeathAnim, position, Quaternion.identity);
    }
    public static GameObject GetLinePrefabInstance()
    {
        return Instantiate(instance.linePrefab);
    }



}
