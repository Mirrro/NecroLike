using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Dropdown[] creatureSlots;
    public GameObject[] allCreatures = new GameObject[3];

    private void Awake()
    {
        if (FindObjectOfType<Loader>() == null)
            Game.Load();
    }

    public void StartLevel()
    {
        Game.LevelStart();
    }
}
