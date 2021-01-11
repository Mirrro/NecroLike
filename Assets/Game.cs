
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
    public static void IncreaseScore(int value)
    {

        Score += value;
    }
    #endregion

    #region Creatures
    public enum Team { Undead, Humans };
    public enum Creature { Skeleton, Knight}

    public static GameObject[] loadout = new GameObject[6];
    public static GameObject[] allCreatures = new GameObject[2];


    public static UnityEvent<Team> DeathEvent = new UnityEvent<Team>();
    public static UnityEvent<Team> SpawnEvent = new UnityEvent<Team>();
    public static void CreatureDeath(Team team)
    {
        DeathEvent.Invoke(team);
    }
    public static void CreatureSpawn(Team team)
    {
        SpawnEvent.Invoke(team);
    }

    public static bool[] unlocked = new bool[] { true, false };
    public static UnityEvent UnlockEvent = new UnityEvent();
    public static void UnlockCreature(Creature creature)
    {
        unlocked[(int)creature] = true;
        UnlockEvent.Invoke();
    }
    #endregion

    #region Input
    public static UnityEvent<Vector3> inputEvent = new UnityEvent<Vector3>();
    public static void FireInput()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);
        inputEvent.Invoke(hit.point);
    }
    public static UnityEvent<int> selectEvent = new UnityEvent<int>();
    public static void SelectCreature(int creature)
    {
        selectEvent.Invoke(creature);
    }
    #endregion

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

    public static Level level;
    public static void InitLevel(Level level)
    {
        Game.level = level;
        var levelStateListeners = Object.FindObjectsOfType<MonoBehaviour>().OfType<ILevelStateListener>();
        foreach (ILevelStateListener s in levelStateListeners)
        {
            level.StateBegin.AddListener(s.OnStateBegin);
            level.StateEnd.AddListener(s.OnStateEnd);
        }
        inputEvent.AddListener(level.OnInput);
        selectEvent.AddListener(level.OnCreatureSelected);
    }
    #endregion


}
