using UnityEngine;
using UnityEngine.UI;
using AIUnits;

public class Menu : MonoBehaviour
{
    public Text runDisplay;

    private void Awake()
    {
        if (FindObjectOfType<Loader>() == null)
        {
            Game.Load();
            return;
        }
        runDisplay.text = Game.highestRun.ToString();
        
        Game.CreateUnit(0);
        Game.CreateUnit(0);
        Game.CreateUnit(0);
        Game.CreateUnit(0);
        Game.CreateUnit(0);
    }
    
    public void StartLevel()
    {
        Game.LoadLevel();
    }
}
