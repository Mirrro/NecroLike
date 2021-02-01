using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class UnitOrderManager : MonoBehaviour
    {
        private void Awake()
        {
            //SpawnShips(Game.LoadShips());
        }

        private void SpawnShips(List<Ship> ships)
        {
            var counter = 0;
            ships.ForEach(s =>
            {
                var ship = GameObject.Instantiate(s.prefab, Vector3.right * (counter * 5), Quaternion.identity);
                ship.GetComponent<ShipBehaviour>().PositionUnits(s);
                counter++;
            });
        }
    }
}