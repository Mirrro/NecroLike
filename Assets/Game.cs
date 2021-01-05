﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AICreatures;

public class Game : MonoBehaviour
{
    public static Game instance;

    public GameObject skellyFab;
    public GameObject skeletonDeathAnim;
    public GameObject abilityButtonPrefab;

    public Transform MOBS;
    public enum Team { Skeletons, Humans };
    public int[] creatureCount = new int[2] { 0, 0 };
    private Vector3 rallyPoint;
    public static Vector3 RallyPoint
    {
        get { return instance.rallyPoint; } 
        set { instance.rallyPoint = value; } 
    }

    public Text humanCountText;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (Game.instance != null)
            Gizmos.DrawWireSphere(RallyPoint, 10);
    }

    private void Awake()
    {
        instance = this;
    }

    public static Transform GetMOBS()
    {
        return instance.MOBS;
    }

    public static void RegisterCreature(int team)
    {
        instance.creatureCount[team]++;
        instance.humanCountText.text = instance.creatureCount[(int)Team.Humans].ToString();
    }

    public static void UnregisterCreature(int team)
    {
        instance.creatureCount[team]--;
        instance.humanCountText.text = instance.creatureCount[(int)Team.Humans].ToString();
    }

    public static void HideButton(GameObject button)
    {
        button.SetActive(false);
    }

    public static GameObject GetSkeletonPrefab()
    {
        return instance.skellyFab;
    }
    public static GameObject GetSkeletonDeathPrefab()
    {
        return instance.skeletonDeathAnim;
    }
    public static GameObject GetAbilityButtonPrefab()
    {
        return instance.abilityButtonPrefab;
    }
    
}
