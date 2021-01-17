using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public PlayerCreature[] creatures;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Game.Init(this);
    }   
}
