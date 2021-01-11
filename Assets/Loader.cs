using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public GameObject[] allCreatures = new GameObject[2];
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Game.allCreatures = allCreatures;
        Game.LoadMenu();
    }   
}
