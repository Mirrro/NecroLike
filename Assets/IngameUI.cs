using AICreatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{
    #region Singleton
    public static IngameUI instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
        }
    }
    #endregion

    #region Creature Cards
    private int usedCards;
    [SerializeField]
    private Button[] creatureSpawnButtons;
    public static Button GetButton(int id)
    {
        return instance.creatureSpawnButtons[id];
    }
    public int selectedButton = -1;
    private void InitCards()
    {
        usedCards = 0;
        for (int i = 0; i < Game.loadout.Length; i++)
            if (Game.loadout[i] != null)
            {
                Game.loadout[i].ConnectToButton(i); // NEEDS TO BE CALLED IN EXTRA METHOD IN ORDER FOR DELEGATES TO WORK!
            }
    }
    public static void SelectCard(int button)
    {
        instance.selectedButton = button;
    }
    public void SpawnCreature(Vector3 pos)
    {
        Game.loadout[selectedButton].SpawnMob(pos);
        creatureSpawnButtons[selectedButton].gameObject.SetActive(false);
        selectedButton = -1;

        usedCards++;
        if (usedCards >= Game.loadout.Length)
            Level.GameState = Game.GameState.Fighting;
    }
    #endregion

    [SerializeField]
    private Text humanCountText;
    public static void UpdateHumanCount(int count)
    {
        instance.humanCountText.text = count.ToString();
    }

    private void Start()
    {
        InitCards();
        Level.GameState = Game.GameState.Fighting;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);
            if (Level.GameState == Game.GameState.Positioning)
            {
                if (selectedButton != -1)
                    SpawnCreature(hit.point);
            }
            else if (Level.GameState == Game.GameState.Fighting)
            {
                Rally(hit.point);
            }
        }
    }

    private void Rally(Vector3 position)
    {
        int layerMask = 1 << 6;
        Collider[] hitColliders = Physics.OverlapSphere(position, 10, layerMask);
        foreach (Collider collider in hitColliders)
        {
            AIRally rally = collider.GetComponent<AIRally>();
            if (rally != null)
                rally.Rally(position);
        };
    }
}
