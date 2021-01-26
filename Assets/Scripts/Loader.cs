using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public PlayerCreature[] creatures;
    public GameObject[] tiles;
    public GameObject[] enemyPrefabs;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Game.Init(this);
    }   
}
