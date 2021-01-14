using UnityEngine.UI;

namespace NecroCore.UI.INGAME
{
    public class UI_SpawnCreaturePanel : UI_InGameComponent
    {
        Button[] creatureSpawnButtons;
        private void Awake()
        {
            creatureSpawnButtons = GetComponentsInChildren<Button>();
            for (int i = 0; i < Game.loadout.Length; i++)
                if (Game.loadout[i] != null)
                    ConnectToButton(i);
            InputHandler.PositionCreatureEvent.AddListener(HideButton);
        }
    
        public void ConnectToButton(int button)
        {
            creatureSpawnButtons[button].onClick.AddListener(delegate { InputHandler.SelectCreature(button); });
        }

        public void HideButton(CreaturePlacementData data)
        {
            creatureSpawnButtons[data.creature].gameObject.SetActive(false);
        }
    }
   
}