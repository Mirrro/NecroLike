using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NecroCore.UI.INGAME
{
    public class UI_SpawnButton : UI_InGameComponent, IDragHandler, IEndDragHandler, IBeginDragHandler, ILevelStateListener
    {
        bool active = false;
        public int slot;
        private GameObject previewCreature;

        private void Awake()
        {
            if(Game.loadout[slot].stats.lostHealth < Game.loadout[slot].stats.health)
                transform.GetChild(0).GetComponent<Image>().sprite = Game.loadout[slot].icon;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!active)
                return;
            Game.handler.Drag();
            transform.GetChild(0).GetComponent<Image>().enabled = false;
            previewCreature = Instantiate(Game.loadout[slot].prefab, Game.handler.worldMousePositon, Quaternion.identity);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!active)
                return;
            previewCreature.transform.position = Game.handler.worldMousePositon;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!active)
                return;
            active = false;
            Game.handler.Release(new CreaturePlacementData(slot, previewCreature));
        }

        public void OnLevelStateEnd(Level.State state)
        {
            if (state == Level.State.Positioning)
                active = false;
        }

        public void OnLevelStateBegin(Level.State state)
        {
            if (Game.loadout[slot].stats.health > 0)
                if (state == Level.State.Positioning)
                    active = true;
        }
    }
}