using AICreatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SpawnCreaturePanel : UI_InGameComponent
{
    #region Singleton

    private static UI_SpawnCreaturePanel instance;
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

    private void Start()
    {
        InitCards();
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
                Level.Rally(hit.point);
            }
        }
    }
    
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
                ConnectToButton(i); // NEEDS TO BE CALLED IN EXTRA METHOD IN ORDER FOR DELEGATES TO WORK!
    }
    public static void SelectCard(int button)
    {
        instance.selectedButton = button;
    }
    public void SpawnCreature(Vector3 pos)
    {
        Level.SpawnMob(Game.loadout[selectedButton], pos);
        creatureSpawnButtons[selectedButton].gameObject.SetActive(false);
        selectedButton = -1;
        usedCards++;
        if (usedCards >= Game.loadout.Length)
            Level.GoToState(Level.State.Fighting);
    }
    #endregion
    
    [SerializeField]
    private Text humanCountText;
    public static void UpdateHumanCount(int count)
    {
        instance.humanCountText.text = count.ToString();
    }

    public void ConnectToButton(int button)
    {
        GetButton(button).onClick.AddListener(delegate { SelectCard(button); });
        GetButton(button).gameObject.SetActive(true);
    }

}
