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
    }

    public static List<Ship> LoadShips()
    {
        var ships = new List<Ship>();
        for (int i = 0; i < Units.Count; i++)
        {
            if (i % 2 !=  0)
            {
                ships[ships.Count -1].load[0] = Units[i];
            }
            else
            {
                ships[ships.Count].load[1] = Units[i];
                ships.Add(new Ship());
            }
        }

        return ships;
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
    public PlayerUnit?[] load;

    public Ship(Ship copy)
    {
        icon = copy.icon;
        prefab = copy.prefab;
        load = new PlayerUnit?[2];
    }
}