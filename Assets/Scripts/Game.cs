using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
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
    
    public static List<PlayerUnit> Units = new List<PlayerUnit>(3);

    public static List<Ship> ships = new List<Ship>();

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
        level.GoToState(Level.State.Playing);
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
        Units.Add(new PlayerUnit(templateUnits[type]));
    }
    public static void UpgradeUnit(PlayerUnit unit, int templateUnitID)
    {
        Units[Units.IndexOf(unit)] = new PlayerUnit(templateUnits[templateUnitID]);
    }

    public static void SwapUnit(PlayerUnit unit1, PlayerUnit unit2)
    {
        var pos1 = Units.IndexOf(unit1);
        var pos2 = Units.IndexOf(unit2);

        Units[pos1] = unit2;
        Units[pos2] = unit1;
        
        Debug.Log("Swapped Unit: " + unit1.name + " with " + unit2.name);
        
        LoadShips();
    }

    public static void LoadShips()
    {
        ships = new List<Ship>();
        ships.Add(new Ship(templateShip));
        var shipCounter = 1;
        for (int u = 0; u < Units.Count; u++)
        {
            for (int s = 0; s < ships[shipCounter-1].load.Length; s++)
            {

                ships[shipCounter-1].load[s] = Units[u];
                if (s == ships[shipCounter-1].load.Length - 1)
                {
                    ships.Add(new Ship(templateShip));
                    shipCounter++;
                }
            }
        }
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
    public Sprite icon;
    public GameObject prefab;
    public PlayerUnit[] load;

    public Ship(Ship copy)
    {
        icon = copy.icon;
        prefab = copy.prefab;
        load = new PlayerUnit[2];
    }
}