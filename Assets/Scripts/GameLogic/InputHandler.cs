using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    public enum SelectionState { None, Dragging}
    public UnityEvent<SelectionState> SelectCreatureEvent = new UnityEvent<SelectionState>();    
    public UnityEvent<CreaturePlacementData> PositionCreatureEvent = new UnityEvent<CreaturePlacementData>();
    public Vector3 worldMousePositon;

    private void Awake()
    {
        Game.InitInputHander(this);
    }
    private void Update()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);
        if(hit.collider != null && hit.collider.tag == "Ground")
            worldMousePositon = new Vector3((int)hit.point.x, (int)hit.point.y, (int)hit.point.z);       
    }

    public void Release(CreaturePlacementData data)
    {
        PositionCreatureEvent.Invoke(data);
        SelectCreatureEvent.Invoke(SelectionState.None);
    }

    public void Drag()
    {
        SelectCreatureEvent.Invoke(SelectionState.Dragging);
    }
}

public struct CreaturePlacementData
{
    public int slot;
    public GameObject creature;
    public CreaturePlacementData(int slot, GameObject creature)
    {
        this.slot = slot;
        this.creature = creature;
    }
}