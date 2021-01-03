using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AICreatures;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Ability[] abilites = new Ability[]{ new AbilitySpawn(), new AbilitySpawn(), new AbilitySpawn() };
    public int selectedAbility = -1;
    public LineRenderer line;

    private void Start()
    {
        for (int i = 0; i<abilites.Length; i++)
        {
            CreateButton(i);
        }
    }
    private void CreateButton(int id)
    {
        GameObject abilityButton = Instantiate(Game.GetAbilityButtonPrefab(), FindObjectOfType<Canvas>().transform);
        abilityButton.transform.position = new Vector3(abilites.Length * 160 - id * 160, 90, 0);
        abilityButton.GetComponent<Button>().onClick.AddListener(delegate { SelectAbility(id); });
        abilityButton.GetComponent<Button>().onClick.AddListener(delegate { Game.HideButton(abilityButton); });
        abilityButton.GetComponentInChildren<Text>().text = abilites[id].GetName();
    }

    private void Update()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);
        line.SetPosition(1, hit.point);
        if (Input.GetMouseButtonDown(0) && selectedAbility != -1)
            ActivateAbility(hit.point);
    }

    public void SelectAbility(int id)
    {
        selectedAbility = id;
        line.enabled = true;
    }

    public void ActivateAbility(Vector3 pos)
    {
        line.enabled = false;
        abilites[selectedAbility].TryActivate(pos);
        selectedAbility = -1;
    }

}

