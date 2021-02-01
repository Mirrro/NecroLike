using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NecroCore.UI.INGAME
{
    public class UI_SpawnButton : UI_InGameComponent, IDragHandler, IEndDragHandler, IBeginDragHandler, ILevelStateListener
    {
        bool active = false;
        public int slot;
        private GameObject previewShip;

        private void Awake()
        {
            if (Game.ships[slot].HasValue)
                transform.GetChild(0).GetComponent<Image>().sprite = Game.ships[slot].Value.icon;
            else
                transform.GetChild(0).GetComponent<Image>().enabled = false;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!active)
                return;
            Game.handler.Drag();
            transform.GetChild(0).GetComponent<Image>().enabled = false;
            previewShip = Instantiate(Game.ships[slot].Value.prefab, Game.handler.worldMousePositon, Quaternion.identity);

        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!active)
                return;
            previewShip.transform.position = Game.handler.worldMousePositon;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!active)
                return;
            active = false;
            Game.handler.Release(new ShipPlacementData(slot, previewShip));
        }

        public void OnLevelStateBegin(Level.State state)
        {
            if (state == Level.State.Playing)
                if (Game.ships[slot].HasValue)
                    active = true;
            
        }

        public void OnLevelStateEnd(Level.State state)
        {
        }
    }
}