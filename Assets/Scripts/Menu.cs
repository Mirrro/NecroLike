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
    }
       
   
    public void StartLevel()
    {
        Game.CreateShip(0);
        Game.LoadLevel();
    }
}
