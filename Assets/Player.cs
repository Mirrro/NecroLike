using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AICreatures;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public CreatureCard[] creatureCards;
    public int selectedCreatureCard = -1;
    public LineRenderer line;

    private void Start()
    {
        for (int i = 0; i< creatureCards.Length; i++)
        {
            CreateCreatureCardButton(i);
        }
    }
    private void CreateCreatureCardButton(int id)
    {
        GameObject abilityButton = Instantiate(Game.GetAbilityButtonPrefab(), FindObjectOfType<Canvas>().transform);
        abilityButton.transform.position = new Vector3(creatureCards.Length * 160 - id * 160, 90, 0);
        abilityButton.GetComponent<Button>().onClick.AddListener(delegate { SelectAbility(id); });
        abilityButton.GetComponent<Button>().onClick.AddListener(delegate { Game.HideButton(abilityButton); });
        abilityButton.GetComponentInChildren<Text>().text = creatureCards[id].spawnPrefab.name;
    }

    private void Update()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);
        line.SetPosition(1, hit.point);
        if (Input.GetMouseButtonDown(0) && selectedCreatureCard != -1)
            SpawnCreature(hit.point);
    }

    public void SelectAbility(int id)
    {
        selectedCreatureCard = id;
        line.enabled = true;
    }

    public void SpawnCreature(Vector3 pos)
    {
        line.enabled = false;
        creatureCards[selectedCreatureCard].SpawnMob(pos);
        selectedCreatureCard = -1;
    }

}

