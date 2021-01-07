using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Dropdown[] creatureSlots;
    public CreatureCard[] cards;
    public List<CreatureCard> unlocked = new List<CreatureCard>();


    private void Awake()
    {
        List<Dropdown.OptionData> data = new List<Dropdown.OptionData>();
        foreach (CreatureCard card in cards)
            if (card.Unlocked)
            {
                unlocked.Add(card);
                data.Add(new Dropdown.OptionData(card.name));
            }

        foreach (Dropdown dd in creatureSlots)
        {
            dd.AddOptions(data);
            SetCreatureCard(dd);
        }

    }

    public void SetCreatureCard(Dropdown dropdown)
    {
        for (int i = 0; i < creatureSlots.Length; i++)
            if (dropdown == creatureSlots[i])
            {
                Game.loadout[i] = unlocked[dropdown.value];
                print(i + " Set to " + unlocked[dropdown.value]);
                return;
            }
    }
    public void StartLevel()
    {
        Game.LevelStart();
    }
}
