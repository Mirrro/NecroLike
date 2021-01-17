using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NecroCore.UI.INGAME
{
    public class UI_RunCounter : UI_InGameComponent
    {
        void Start()
        {
            GetComponent<Text>().text = Game.run.ToString();
        }
    }
}