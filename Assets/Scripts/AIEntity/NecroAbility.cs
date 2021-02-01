using AIUnits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroAbility : CreatureAbility
{
    [SerializeField] protected GameObject skeletonPrefab;
    public override void TriggerAbility(AIEntity kill)
    {
        Instantiate(skeletonPrefab, kill.transform.position, Quaternion.identity);
    }
}
