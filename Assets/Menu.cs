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
        foreach (CreatureCard card in Game.GetUnlockedCreatureCards())
            data.Add(new Dropdown.OptionData(card.name));
        foreach (Dropdown dd in creatureSlots)
            dd.AddOptions(data);
    }

    public void SetCreatureCard(Dropdown dropdown)
    {
        for (int i = 0; i < creatureSlots.Length; i++)
            if (dropdown == creatureSlots[i])
            {
                Game.SetCreatureCard(i, dropdown.value);
                return;
            }
    }
    public void StartLevel()
    {
        Game.LevelStart();
    }
}
