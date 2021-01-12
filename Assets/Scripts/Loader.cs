using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public GameObject[] allCreatures;
    public Sprite[] creatureIcons;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Game.creaturePrefabs = allCreatures;
        Game.creatureIcons = creatureIcons;
        Game.LoadMenu();
    }   
}
