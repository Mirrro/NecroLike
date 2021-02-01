using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    public enum SelectionState { None, Dragging}
    public UnityEvent<SelectionState> SelectShipEvent = new UnityEvent<SelectionState>();    
    public UnityEvent<ShipPlacementData> PositionShipEvent = new UnityEvent<ShipPlacementData>();
    public Vector3 worldMousePositon;

    private void Awake()
    {
        Game.InitInputHander(this);
    }
    private void Update()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);
        if (hit.collider != null && hit.collider.tag == "Ground")
            worldMousePositon = hit.point;  
    }

    public void Release(ShipPlacementData data)
    {
        PositionShipEvent.Invoke(data);
        SelectShipEvent.Invoke(SelectionState.None);
    }

    public void Drag()
    {
        SelectShipEvent.Invoke(SelectionState.Dragging);
    }
}

public struct ShipPlacementData
{
    public int slot;
    public GameObject ship;
    public ShipPlacementData(int slot, GameObject ship)
    {
        this.slot = slot;
        this.ship = ship;
    }
}