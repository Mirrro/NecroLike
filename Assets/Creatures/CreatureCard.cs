using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CreatureCard : ScriptableObject
{
    [MenuItem("Assets/Create/CreatureCard")]
    public static void CreateCreatureCard()
    {
        CreatureCard asset = CreateInstance<CreatureCard>();

        AssetDatabase.CreateAsset(asset, "Assets/NewScripableObject.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    private GameObject spawnPrefab;
    [SerializeField]
    private int spawnAmount;
    [SerializeField]
    private bool unlocked;
    public bool Unlocked
    {
        get
        {return unlocked;}
        set
        {unlocked = value;}
    }
    public void SpawnMob(Vector3 position)
    {
        for(int i = 0; i<spawnAmount; i++)
            Instantiate(spawnPrefab, position, Quaternion.identity, Level.GetMobs());
    }
    public void CreateCreatureCardObject(int id)
    {
        GameObject card = Instantiate(cardPrefab, FindObjectOfType<Canvas>().transform);
        card.transform.position = new Vector3(Game.LoadoutSize() * 160 - id * 160, 90, 0);
        card.GetComponent<Button>().onClick.AddListener(delegate { Level.SelectCard(id); });
        card.GetComponent<Button>().onClick.AddListener(delegate { card.SetActive(false); });
        card.GetComponentInChildren<Text>().text = spawnPrefab.name;
    }

}
