using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour, ILevelStateListener
{
    private static int selectedCreature = -1;
    public static UnityEvent<CreaturePlacementData> PositionCreatureEvent = new UnityEvent<CreaturePlacementData>();
    public static UnityEvent<Vector3> InputEvent = new UnityEvent<Vector3>();

    bool allowPlacement;
    bool allowRallying;

    private void Awake()
    {
        InputEvent.AddListener(OnInput);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            FireInput();
    }

    public static void FireInput()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);
        InputEvent.Invoke(hit.point);
    }

    public void OnInput(Vector3 input)
    {
        if (allowPlacement && selectedCreature != -1)
        {
            PositionCreatureEvent.Invoke(new CreaturePlacementData(selectedCreature, input));
            selectedCreature = -1;
        }
        else if (allowRallying)
            Game.level.Rally(input);
    }

    public static void SelectCreature(int creature)
    {
        selectedCreature = creature;
    }

    public void OnStateEnd(Level.State state)
    {
        if (state == Level.State.Positioning)
            allowPlacement = false;
        else if (state == Level.State.Fighting)
            allowRallying = false;
    }

    public void OnStateBegin(Level.State state)
    {
        if (state == Level.State.Positioning)
            allowPlacement = true;
        else if (state == Level.State.Fighting)
            allowRallying = true;
    }
}

public struct CreaturePlacementData
{
    public int creature;
    public Vector3 position;
    public CreaturePlacementData(int creature, Vector3 position)
    {
        this.creature = creature;
        this.position = position;
    }
}