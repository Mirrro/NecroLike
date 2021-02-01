using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using AIUnits;
using System.Collections.Generic;

public static class Game
{
    #region Score
    public static int highestRun = 1;
    public static int run = 1;
    private static int score;
    public static UnityEvent scoreEvent;
    public static int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            scoreEvent.Invoke();
        }
    }
    #endregion

    public static void Init(Loader loader)
    {
        templateUnits = loader.units;
        templateShip = loader.ship;
        LevelGenerator.Init(loader.tiles, loader.enemyPrefabs);
        LoadMenu();
    }
    public enum Team { Vikings, Christians};
    public static PlayerUnit[] templateUnits;
    public static Ship templateShip;

    public static Ship?[] ships = new Ship?[5];
    public static int NumberOfShips
    {
        get
        {
            int count = 0;
            foreach (Ship? ship in ships)
                if (ship.HasValue)
                    count++;
            return count;
        }
    }

    #region Loading
    public static void Load()
    {
        SceneManager.LoadScene(0);
    }
    public static void LoadMenu()
    {
        SceneManager.LoadScene(1);
    }
    public static void LoadLevel()
    {
        SceneManager.LoadScene(2);
    }
    #endregion

    #region Level Initialisation
    public static Level level;
    public static InputHandler handler;
    public static void InitLevel(Level level)
    {
        Game.level = level;

        level.LevelStateBegin.AddListener(OnLevelStateChange);
        var levelStateListeners = Object.FindObjectsOfType<MonoBehaviour>().OfType<ILevelStateListener>();
        foreach (ILevelStateListener s in levelStateListeners)
        {
            level.LevelStateBegin.AddListener(s.OnLevelStateBegin);
            level.LevelStateEnd.AddListener(s.OnLevelStateEnd);
        }
        level.GoToState(Level.State.Positioning);
    }
    public static void InitInputHander(InputHandler handler)
    {
        Game.handler = handler;

        var selectionStateListeners = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISelectionStateListener>();
        foreach (ISelectionStateListener s in selectionStateListeners)
               handler.SelectShipEvent.AddListener(s.OnSelectionState);

        var placementListeners = Object.FindObjectsOfType<MonoBehaviour>().OfType<IPlacementListener>();
        foreach (IPlacementListener s in placementListeners)
            handler.PositionShipEvent.AddListener(s.OnShipPlacement);
        
    }
    public static void OnLevelStateChange(Level.State state)
    {
       
    }
    #endregion

    public static void CreateUnit(int type)
    {
        for(int s = 0; s < ships.Length; s++)
        {
            Ship ship;
            if (!ships[s].HasValue)
                ship = new Ship(templateShip);
            else
                ship = ships[s].Value;

            for (int i = 0; i < ship.size; i++)
            {
                if (!ship.load[i].HasValue)
                {
                    ship.load[i] = new PlayerUnit(templateUnits[type]);
                    return;
                }
            }
        }        
    }
    public static void UpgradeUnit(int ship, int position, int newUnit)
    {
        ships[ship].Value.load[position] = new PlayerUnit(templateUnits[newUnit]);
    }
}

[System.Serializable]
public struct PlayerUnit
{
    public string name;
    public GameObject prefab;
    public Stats stats;

    public PlayerUnit(PlayerUnit copy)
    {
        name = copy.name;
        stats = copy.stats;
        prefab = copy.prefab;
    }
}

[System.Serializable]
public struct Ship
{
    public int size;
    public Sprite icon;
    public GameObject prefab;
    public PlayerUnit?[] load;

    public Ship(Ship copy)
    {
        size = copy.size;
        icon = copy.icon;
        prefab = copy.prefab;
        load = new PlayerUnit?[size];
    }
}