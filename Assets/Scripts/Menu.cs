using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject creatureIconPrefab;

    private void Awake()
    {
        if (FindObjectOfType<Loader>() == null)
            Game.Load();
        CreateCreatureIcons();
        Game.loadout = new GameObject[] { Game.creaturePrefabs[0], Game.creaturePrefabs[0], Game.creaturePrefabs[0], Game.creaturePrefabs[0], Game.creaturePrefabs[0], Game.creaturePrefabs[0] };
    }

    public void SetCreature(int slot, int creature)
    {
        Game.loadout[slot] = Game.creaturePrefabs[creature];
    }

    public void CreateCreatureIcons()
    {
        for (int i = 0; i<Game.creaturePrefabs.Length; i++)
        {            
            int x = (i % 5) * 120 - 200;
            int y = (i / 5) * 120 + 200;
            GameObject creatureIcon = Instantiate(creatureIconPrefab, new Vector3(x, y, 0) - transform.worldToLocalMatrix.MultiplyPoint(new Vector3(0, 0, 0)), Quaternion.identity, transform);
            creatureIcon.transform.GetChild(0).GetComponent<Image>().sprite = Game.creatureIcons[i];
        }
    }

    public void SelectCreature(int creature)
    {

    }

    public void StartLevel()
    {
        Game.LoadLevel();
    }
}
