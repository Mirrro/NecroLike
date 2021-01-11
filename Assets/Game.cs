using System.Collections;
using System.Collections.Generic;
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
    public enum Team { Skeletons, Humans };
    public enum Creature { Skeleton, Knight}
    public static GameObject[] loadout = new GameObject[6];
    public static GameObject[] allCreatures = new GameObject[2];

    public static bool[] unlocked = new bool[] { true, false };
    public static UnityEvent unlockEvent = new UnityEvent();
    public static void UnlockCreature(Creature creature)
    {
        unlocked[(int)creature] = true;
        unlockEvent.Invoke();
    }
    #endregion

    public static Level level;

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
    public static void StartLevel()
    {

    }

}
