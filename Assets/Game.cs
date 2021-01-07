using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class Game
{

    public enum Team { Skeletons, Humans };
    public enum GameState { Entry, Positioning, Fighting};

    public static CreatureCard[] loadout = new CreatureCard[3];

    public static void LevelFinished()
    {
        SceneManager.LoadScene("Menu");
    }
    public static void LevelStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
