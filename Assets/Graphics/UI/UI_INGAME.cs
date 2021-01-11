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
                    _instance = FindObjectOfType<UI_INGAME>();

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
            AddToSkullScore(100);

        }

        #endregion

        #region Events
        
        public event EventHandler<UI_ScoreEventArgs> EventAddToSkullScore;
        public event EventHandler<UI_ScoreEventArgs> EventUpdateCoinScore;
        public event EventHandler EventGamePaused;
        public event EventHandler EventGameEnd;

        #endregion


        #region InvokeEventFunctions

        public static void AddToSkullScore(int value)
        {
            var data = new UI_ScoreEventArgs {Score = value};

            Instance.EventAddToSkullScore?.Invoke(Instance, data);
        }

        public void UpdateCoinScore(int value)
        {
            var data = new UI_ScoreEventArgs {Score = value};

            Instance.EventUpdateCoinScore?.Invoke(Instance, data);
        }
        
        public void PauseGame()
        {
            Instance.EventGamePaused?.Invoke(Instance, EventArgs.Empty);
        }
        
        public void EndGame(int value)
        {
            var data = new UI_ScoreEventArgs {Score = value};

            Instance.EventUpdateCoinScore?.Invoke(Instance, data);
        }

        #endregion

    }
    
}

