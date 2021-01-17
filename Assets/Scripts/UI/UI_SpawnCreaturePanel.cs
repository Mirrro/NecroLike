using UnityEngine.UI;
using UnityEngine;

namespace NecroCore.UI.INGAME
{
    public class UI_SpawnCreaturePanel : UI_InGameComponent
    {
        Button[] creatureSpawnButtons;
        private void Awake()
        {
            creatureSpawnButtons = GetComponentsInChildren<Button>();
            for (int i = 0; i < Game.loadout.Length; i++)
                if (Game.loadout[i].stats.health >0)
                    ConnectToButton(i);
            InputHandler.PositionCreatureEvent.AddListener(HideButton);
        }
    
        public void ConnectToButton(int button)
        {
            if (Game.loadout[button].stats.health <= 0)
                creatureSpawnButtons[button].transform.GetChild(0).GetComponent<Image>().color = Color.red;
            else
                creatureSpawnButtons[button].onClick.AddListener(delegate { InputHandler.SelectCreature(button); });
            creatureSpawnButtons[button].transform.GetChild(0).GetComponent<Image>().sprite = Game.loadout[button].icon;

        }

        public void HideButton(CreaturePlacementData data)
        {
            creatureSpawnButtons[data.creature].gameObject.SetActive(false);
        }
    }
   
}