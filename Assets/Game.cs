using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance;
    private void Awake()
    {
        instance = this;
    }
    public static bool infected;
    public GameObject skellyFab;
}
