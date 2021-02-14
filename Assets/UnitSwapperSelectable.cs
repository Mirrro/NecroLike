using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Collider))]
    public class UnitSwapperSelectable : MonoBehaviour
    {
        public PlayerUnit unit;
        public UnitSwapper swapper;

        private event SelectedEventHandler selectedEvent;
        delegate void SelectedEventHandler(UnitSwapperSelectable sender);
        private void Awake()
        {
            swapper = FindObjectOfType<UnitSwapper>();
            if (swapper)
            {
                RegisterEvents();
            }
        }

        private void RegisterEvents()
        {
            selectedEvent += swapper.OnSelection;
        }

        private void OnMouseDown()
        {
            Debug.Log("clickedOnme");
            selectedEvent.Invoke(this);
        }

        private bool isSelected()
        {
            return swapper.GetSelection() == this;
        }
    }
    
}