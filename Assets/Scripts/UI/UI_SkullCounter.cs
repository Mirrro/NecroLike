using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace NecroCore.UI.INGAME
{
    public class UI_SkullCounter : UI_InGameComponent
    {
        [SerializeField] private int currentScore = 100;
        [SerializeField] private float countsPerSecond = 10;
        [SerializeField] private Text text;
        [SerializeField] private RectTransform Icon;
        
        private Vector3 startScale = Vector3.one;

        private void Awake()
        {
            UI_INGAME.Instance.EventAddToSkullScore += AddToScore;
            startScale = Icon.localScale;
        }
        private void Start()
        {
            UI_INGAME.AddToSkullScore(10);
        }
        
        private void AddToScore(object sender, UI_ScoreEventArgs args)
        {
            text.text = (currentScore + args.Score).ToString();
            StartCoroutine(AddAnimation(args.Score, 1));
        }

        private IEnumerator AddAnimation(int power, float time)
        {
            Vector3 newScale = startScale * (power / 10);
            newScale.x = newScale.y = Mathf.Clamp(newScale.x, 1, 1.4f);
            float elapsedTime = 0;
            while (elapsedTime < time)
            {
                Icon.localScale = new Vector3(startScale.x + Mathf.PingPong(0, newScale.x), startScale.y + Mathf.PingPong(0, newScale.y), Icon.localScale.z);
                elapsedTime += Time.deltaTime;
                
                yield return null;
            }  
            // Make sure we got there
            Icon.localScale = startScale;
            yield return null;
        }
        
    }
}

