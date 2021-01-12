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
    #endregion
    
    public enum Team { Undead, Humans };
    public enum Creature { Skeleton, Knight }
    public static GameObject[] creaturePrefabs;
    public static Sprite[] creatureIcons;
    
    public static GameObject[] loadout = new GameObject[6];

    public static bool[] unlocked = new bool[] { true, false };
    public static UnityEvent UnlockEvent = new UnityEvent();
    public static void UnlockCreature(Creature creature)
    {
        unlocked[(int)creature] = true;
        UnlockEvent.Invoke();
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

    public static Level level;
    public static void InitLevel(Level level)
    {
        Game.level = level;
        var levelStateListeners = Object.FindObjectsOfType<MonoBehaviour>().OfType<ILevelStateListener>();
        foreach (ILevelStateListener s in levelStateListeners)
            Level.InitStateListener(s);
        level.GoToState(Level.State.Positioning);
    }
    #endregion


}
