using System;
using UnityEngine;

public class PillarLoadOut : MonoBehaviour
{
    [SerializeField] private GameObject upgradePanel;
    private void OnMouseDown()
    {
        upgradePanel.SetActive(true);
    }
}
