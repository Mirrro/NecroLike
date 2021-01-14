using UnityEngine;
using UnityEngine.UI;

namespace NecroCore.UI.INGAME
{
    public class UI_SkullCounter : UI_InGameComponent
    {
        [SerializeField] private int currentScore = 100;
        [SerializeField] private float countsPerSecond = 10;
        [SerializeField] private Text text;
        private void Start()
        {
            UI_INGAME.Instance.EventAddToSkullScore += AddToScore;
        }
        
        private void AddToScore(object sender, UI_ScoreEventArgs args)
        {
            text.text = (currentScore + args.Score).ToString();
        }
        
    }
}

