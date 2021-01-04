using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CreatureCard : ScriptableObject
{
    [MenuItem("Assets/Create/AbilitySpawnValues")]
    public static void CreateAbilitySpawnValue()
    {
        CreatureCard asset = CreateInstance<CreatureCard>();

        AssetDatabase.CreateAsset(asset, "Assets/NewScripableObject.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    public Image buttonImage;
    public GameObject spawnPrefab;
    public int spawnAmount;

    public void SpawnMob(Vector3 position)
    {
        for(int i = 0; i<spawnAmount; i++)
            Instantiate(spawnPrefab, position, Quaternion.identity, Game.GetMOBS());
    }
}
