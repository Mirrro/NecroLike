using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AICreatures;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class Level : MonoBehaviour
{
    #region Singleton
    public static Level instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
        }
    }
    #endregion

    #region Creature Cards
    public int selectedCreatureCard = -1;
    private void InitCards()
    {
        for (int i = 0; i < Game.LoadoutSize(); i++)
            Game.GetCreatureCard(i).CreateCreatureCardObject(i);
    }
    public static void SelectCard(int id)
    {
        instance.selectedCreatureCard = id;
    }
    public void SpawnCreature(Vector3 pos)
    {
        Game.GetCreatureCard(selectedCreatureCard).SpawnMob(pos);
        selectedCreatureCard = -1;
    }
    #endregion

    private void Start()
    {
        InitCards();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);
            if (selectedCreatureCard == -1)
                Rally(hit.point);
            else
                SpawnCreature(hit.point);
        }
    }

    private void Rally(Vector3 position)
    {
        int layerMask = 1 << 6;
        Collider[] hitColliders = Physics.OverlapSphere(position, 10, layerMask);
        foreach (Collider collider in hitColliders)
        {
            AIRally rally = collider.GetComponent<AIRally>();
            if (rally != null)
                rally.Rally(position);
        };
    }


    #region Creature Tracking
    [SerializeField]
    private Text humanCountText;
    [SerializeField]
    private Transform mobs;
    public static Transform GetMobs()
    {
        return instance.mobs;
    }
    private int[] creatureCount = new int[2] { 0, 0 };
    public static void RegisterCreature(int team)
    {
        instance.creatureCount[team]++;
        instance.humanCountText.text = instance.creatureCount[(int)Game.Team.Humans].ToString();
    }

    public static void UnregisterCreature(int team)
    {
        instance.creatureCount[team]--;
        instance.humanCountText.text = instance.creatureCount[(int)Game.Team.Humans].ToString();
        if (instance.creatureCount[team] <= 0)
            Game.LevelFinished();
    }
    #endregion
}
