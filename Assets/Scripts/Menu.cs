using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject creatureIconPrefab;
    public Transform allCreaturePanel;
    private int selectedCreature = -1;

    private void Awake()
    {
        if (FindObjectOfType<Loader>() == null)
        {
            Game.Load();
            return;
        }
        CreateCreatureIcons();
        Game.loadout = new GameObject[] { Game.creaturePrefabs[0], Game.creaturePrefabs[0], Game.creaturePrefabs[0], Game.creaturePrefabs[0], Game.creaturePrefabs[0], Game.creaturePrefabs[0] };
    }

    public void SetCreature(int slot)
    {
        print(slot);
        if(selectedCreature != -1)
            Game.loadout[slot] = Game.creaturePrefabs[selectedCreature];
    }

    public void SetSelectedIcon(Image image)
    {
        if (selectedCreature != -1)
            image.sprite = Game.creatureIcons[selectedCreature];
    }

    public void CreateCreatureIcons()
    {
        for (int i = 0; i<Game.creaturePrefabs.Length; i++)
        {
            CreateCreatureIcon(i);
        }
    }
    private void CreateCreatureIcon(int i)
    {
        int x = (i % 5) * 120 - 200;
        int y = (i / 5) * 120 + 200;
        GameObject creatureIcon = Instantiate(creatureIconPrefab, Vector3.zero, Quaternion.identity, allCreaturePanel);
        creatureIcon.transform.GetChild(0).GetComponent<Image>().sprite = Game.creatureIcons[i];
        creatureIcon.GetComponent<Button>().onClick.AddListener(delegate { SelectCreature(i); });
    }

    public void SelectCreature(int creature)
    {
        print(creature);
        selectedCreature = creature;
    }

    public void StartLevel()
    {
        Game.LoadLevel();
    }
}
