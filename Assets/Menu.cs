using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Dropdown[] creatureSlots;

    private void Awake()
    {
        List<Dropdown.OptionData> data = new List<Dropdown.OptionData>();
        foreach (CreatureCard card in Game.GetAllCreatureCards())
            if(card.Unlocked)
                data.Add(new Dropdown.OptionData(card.name));
        foreach (Dropdown dd in creatureSlots)
            dd.AddOptions(data);
    }
}
