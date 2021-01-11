using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Dropdown[] creatureSlots;

    private void Awake()
    {
        if (FindObjectOfType<Loader>() == null)
            Game.Load();
        Game.loadout = new GameObject[] { Game.allCreatures[0], Game.allCreatures[0], Game.allCreatures[0], Game.allCreatures[0], Game.allCreatures[0], Game.allCreatures[0] };
    }

    public void StartLevel()
    {
        Game.LoadLevel();
    }
}
