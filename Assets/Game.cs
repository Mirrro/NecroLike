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
    
    private void Awake()
    {
        instance = this;
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
