using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    #region Singleton
    public static Game instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    public enum Team { Skeletons, Humans };
    private CreatureCard[] loadout = new CreatureCard[3];
    public CreatureCard[] allCards;

    public static CreatureCard[] GetUnlockedCreatureCards()
    {
        List<CreatureCard> unlocked = new List<CreatureCard>();
        foreach (CreatureCard card in instance.allCards)
            if (card.Unlocked)
                unlocked.Add(card);
        return unlocked.ToArray();
    }
    public static int LoadoutSize()
    {
        return instance.loadout.Length;
    }
    public static CreatureCard GetCreatureCard(int id)
    {
        return instance.loadout[id];
    }
    public static CreatureCard SetCreatureCard(int id, int type)
    {
        return instance.loadout[id] = instance.allCards[type];
    }
    public static void LevelFinished()
    {
        SceneManager.LoadScene("Menu");
    }
    public static void LevelStart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void UnlockCard(CreatureCard card)
    {
        card.Unlocked = true;
    }

}
