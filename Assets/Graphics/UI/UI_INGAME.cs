using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace NecroCore.UI.INGAME
{
    public class UI_INGAME : MonoBehaviour
    {
        #region Singelton
        
        private static UI_INGAME _instance;

        public static UI_INGAME Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<UI_INGAME>();

                    if (_instance == null)
                    {
                        GameObject container = new GameObject("[new Singleton]");
                        _instance = container.AddComponent<UI_INGAME>();
                    }
                }

                return _instance;
            }
        }

        private void Start()
        {
            PauseGame();
        }

        #endregion

        #region Events
        
        public event EventHandler<UI_ScoreEventArgs> EventUpdateSkullScore;
        public event EventHandler<UI_ScoreEventArgs> EventUpdateCoinScore;
        public event EventHandler EventGamePaused;
        public event EventHandler EventGameEnd;

        #endregion

        #region InvokeEventFunctions

        public void UpdateSkullScore(int value)
        {
            var data = new UI_ScoreEventArgs {Score = value};

            EventUpdateSkullScore?.Invoke(this, data);
        }

        public void UpdateCoinScore(int value)
        {
            var data = new UI_ScoreEventArgs {Score = value};

            EventUpdateCoinScore?.Invoke(this, data);
        }
        
        public void PauseGame()
        {
            EventGamePaused?.Invoke(this, EventArgs.Empty);
        }
        
        public void EndGame(int value)
        {
            var data = new UI_ScoreEventArgs {Score = value};

            EventUpdateCoinScore?.Invoke(this, data);
        }

        #endregion
        
    }
    
}

