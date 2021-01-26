using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using AICreatures;
using System.Collections.Generic;

public static class Game
{
    #region Score
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
        templateCreatures = loader.creatures;
        LevelGenerator.Init(loader.tiles, loader.enemyPrefabs);
        LoadMenu();
    }
    public enum Team { Undead, Humans };
    public static PlayerCreature[] templateCreatures;
    /*public static PlayerCreature GetCreature(int type)
    {
        return new PlayerCreature
        {
            type = templateCreatures[type].type,
            icon = templateCreatures[type].icon,
            prefab = templateCreatures[type].prefab,
            stats = templateCreatures[type].stats,
            unlocked = templateCreatures[type].unlocked
        };
    }*/
    public static int highestRun = 1;
    public static int run = 1;
    public static PlayerCreature[] loadout = new PlayerCreature[6];
    public static int AvailableCreatures
    {
        get
        {
            int count = 0;
            for (int i = loadout.Length; i-- != 0;)
                if (loadout[i].stats.health>0)
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

    #region Initialisation
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
               handler.SelectCreatureEvent.AddListener(s.OnSelectionState);

        var placementListeners = Object.FindObjectsOfType<MonoBehaviour>().OfType<IPlacementListener>();
        foreach (IPlacementListener s in placementListeners)
            handler.PositionCreatureEvent.AddListener(s.OnCreaturePlacement);
        
    }
    public static void OnLevelStateChange(Level.State state)
    {
        if (state == Level.State.End)
        {
            int notDead = 0;
            for (int i = 0; i < level.inGameLoadout.Length; i++)
            {
                loadout[i].stats = level.inGameLoadout[i].stats;
                if (loadout[i].stats.lostHealth < loadout[i].stats.health)
                    notDead++;
            }
            if(notDead>0)
            {
                run++;
                LoadLevel();
            }
            else
            {
                if(run> highestRun)
                    highestRun = run;
                run = 1;
                LoadMenu();
            }
        }
    }
    #endregion

}

[System.Serializable]
public struct PlayerCreature
{
    public string name;
    public Sprite icon;
    public GameObject prefab;
    public Stats stats;
    public bool unlocked;
}