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

        AssetDatabase.CreateAsset(asset, "Assets/CreatureCard.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
    
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
    
    public void ConnectToButton(int button)
    {
        IngameUI.GetButton(button).onClick.AddListener(delegate { IngameUI.SelectCard(button); });
        IngameUI.GetButton(button).gameObject.SetActive(true);
    }
    public void SpawnMob(Vector3 position)
    {
        for (int i = 0; i < spawnAmount; i++)
            Instantiate(spawnPrefab, position, Quaternion.identity, Level.Mobs);
    }
}
