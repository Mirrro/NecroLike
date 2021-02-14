using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class UnitSwapper : MonoBehaviour
{
    
    #region Singelton
        
    private static UnitSwapper _instance;

    public static UnitSwapper Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UnitSwapper>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("[new UnitSwapper]");
                    _instance = container.AddComponent<UnitSwapper>();
                }
            }

            return _instance;
        }
    }
    #endregion

    [SerializeField] private UnitSwapperSelectable selection;
    [SerializeField] private UnitSwapperSelectable target;

    private bool hasSelection;
    
    private void Awake()
    {
        Game.CreateUnit(0);
        Game.CreateUnit(0);
        Game.CreateUnit(0);
        Game.CreateUnit(0);
        Game.CreateUnit(0);
        
        Game.LoadShips();
    }

    private void Start()
    {
        StartCoroutine(SpawnShipsSafe());
    }

    private IEnumerator SpawnShipsSafe()
    {
        yield return new WaitUntil(() => Game.ships.Count > 0);
        SpawnShips(Game.ships);
    }

    public void OnSelection(UnitSwapperSelectable sender)
    {
        Debug.Log("new selection arrived");
        var selectable = sender;
        if (!hasSelection)
        {
            hasSelection = true;
            selection = selectable;
        }
        else if(selectable != selection)
        {
            Debug.Log("start swapping");
            target = selectable;
            Swap();
            selection = null;
            target = null;
            hasSelection = false;
        }
        else
        {
            selection = null;
            hasSelection = false;
        }
    }

    public UnitSwapperSelectable GetSelection()
    {
        return selection;
    }
    
    private void SpawnShips(List<Ship> ships)
    {
        var counter = 0;
        ships.ForEach(s =>
        {
            var ship = GameObject.Instantiate(s.prefab, Vector3.right * (counter * 5), Quaternion.identity);
            ship.GetComponent<ShipBehaviour>().PositionUnits(s,true);
            counter++;
        });
    }

    private void Swap()
    {
        Game.SwapUnit(selection.unit,target.unit);
        
    }
}
