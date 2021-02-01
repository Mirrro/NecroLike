using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlacementListener
{
    void OnShipPlacement(ShipPlacementData data);
}
