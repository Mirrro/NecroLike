using System;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

namespace NecroCore.UI.INGAME
{
    public class UI_BottomPanel : UI_InGameComponent
    {
        private void Awake()
        {
            UI_INGAME.Instance.EventGamePaused += OnPause;

        }

        private void OnPause(object sender, EventArgs args)
        {
            Debug.Log("GamePaused");
        }
        
    }
}

