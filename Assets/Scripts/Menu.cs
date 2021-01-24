using UnityEngine;
using UnityEngine.UI;
using AICreatures;

public class Menu : MonoBehaviour
{
    public GameObject creatureIconPrefab;
    public Transform allCreaturePanel;
    private int selectedCreature = 0;
    public GameObject previewPeddle;
    public Text nameDisplay;
    public Text healthDisplay;
    public Text damageDisplay;
    public Text rangeDisplay;
    public Text speedDisplay;
    public Text runDisplay;

    private void Awake()
    {
        if (FindObjectOfType<Loader>() == null)
        {
            Game.Load();
            return;
        }
        CreateCreatureIcons();
        Game.loadout[0] = Game.templateCreatures[0];
        Game.loadout[1] = Game.templateCreatures[0];
        Game.loadout[2] = Game.templateCreatures[0];
        Game.loadout[3] = Game.templateCreatures[0];
        Game.loadout[4] = Game.templateCreatures[0];
        Game.loadout[5] = Game.templateCreatures[0];

        SelectCreature(0);
        runDisplay.text = Game.highestRun.ToString();
    }

    public void SetCreature(int slot)
    {
        Game.loadout[slot] = Game.templateCreatures[selectedCreature];
    }

    public void SetSelectedIcon(Image image)
    {
        image.sprite = Game.templateCreatures[selectedCreature].icon;
    }

    public void CreateCreatureIcons()
    {
        for (int i = 0; i < Game.templateCreatures.Length; i++)
            CreateCreatureIcon(i);
    }
    private void CreateCreatureIcon(int i)
    {
        int x = (i % 5) * 120 - 200;
        int y = (i / 5) * 120 + 200;
        GameObject creatureIcon = Instantiate(creatureIconPrefab, allCreaturePanel.position, Quaternion.identity, allCreaturePanel);
        creatureIcon.transform.GetComponent<Image>().sprite = Game.templateCreatures[i].icon;
        creatureIcon.GetComponent<Button>().onClick.AddListener(delegate { SelectCreature(i); });
    }

    public void SelectCreature(int creature)
    {
        selectedCreature = creature;
        if(previewPeddle.transform.childCount>0)
            Destroy(previewPeddle.transform.GetChild(0).gameObject);

        GameObject preview = Instantiate(Game.templateCreatures[selectedCreature].prefab, previewPeddle.transform);
        preview.GetComponent<AIEntity>().enabled = false;
        preview.GetComponent<AICombat>().enabled = false;
        preview.GetComponent<DefaultBehaviour>().enabled = false;

        nameDisplay.text = Game.templateCreatures[selectedCreature].name;
        healthDisplay.text = Game.templateCreatures[selectedCreature].stats.health.ToString();
        damageDisplay.text = Game.templateCreatures[selectedCreature].stats.damage.ToString();
        rangeDisplay.text = Game.templateCreatures[selectedCreature].stats.range.ToString();
        speedDisplay.text = Game.templateCreatures[selectedCreature].stats.speed.ToString();
    }

    public void StartLevel()
    {
        Game.LoadLevel();
    }
}
