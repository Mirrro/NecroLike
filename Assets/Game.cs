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
    private CreatureCard[] loadout;
    public CreatureCard[] allCards;

    public static CreatureCard[] GetAllCreatureCards()
    {
        return instance.allCards;
    }
    public static int LoadoutSize()
    {
        return instance.loadout.Length;
    }
    public static CreatureCard GetCreatureCard(int id)
    {
        return instance.loadout[id];
    }
    public static void LevelFinished()
    {
        SceneManager.LoadScene("Menu");
    }
    public void UnlockCard(CreatureCard card)
    {
        card.Unlocked = true;
    }

}
