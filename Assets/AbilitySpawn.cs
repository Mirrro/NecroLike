using UnityEngine;

public class AbilitySpawn : Ability
{
    public override string GetName()
    {
        return "Spawn Skeleton";
    }

    protected override void Activate(Vector3 position)
    {
        Game.Instantiate(Game.GetSkeletonPrefab(),position, Quaternion.identity, Game.GetMOBS());
    }
}
