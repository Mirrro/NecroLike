using AICreatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SpawnCreaturePanel : UI_InGameComponent
{
    private void Awake()
    {
        InitButtons();
    }

    
    
    private int usedCards;


    private void InitButtons()
    {
        usedCards = 0;
        for (int i = 0; i < Game.loadout.Length; i++)
            if (Game.loadout[i] != null)
                ConnectToButton(i); // NEEDS TO BE CALLED IN EXTRA METHOD IN ORDER FOR DELEGATES TO WORK!
    }
    public  void SelectCard(int button)
    {
        
    }
    
    [SerializeField]
    private Text humanCountText;
    public  void UpdateHumanCount(int count)
    {
        humanCountText.text = count.ToString();
    }
    [SerializeField] private Button[] creatureSpawnButtons;
    public void ConnectToButton(int button)
    {
        creatureSpawnButtons[button].onClick.AddListener(delegate { SelectCard(button); });
        creatureSpawnButtons[button].gameObject.SetActive(true);
    }

}
