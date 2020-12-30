using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance;
    public static bool infected;
    public GameObject skellyFab;
    public Player player;

    private void Awake()
    {
        instance = this;
    }
    public static Player GetPlayer()
    {
        return instance.player;
    }
    public static GameObject GetSkellyFab()
    {
        return instance.skellyFab;
    }
   
}
