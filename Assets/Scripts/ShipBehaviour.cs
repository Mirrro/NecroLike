using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBehaviour : MonoBehaviour
{
    private Vector3 target;
    private bool initialized = false;
    public void Init(Ship ship)
    {
        transform.LookAt(Game.level.transform);
        transform.Rotate(Vector3.up * 90);
        Physics.Raycast(new Ray(transform.position, -transform.right), out RaycastHit hit);
        target = hit.point;
        PositionUnits(ship);
        initialized = true;
    }

    public void PositionUnits(Ship ship)
    {
        for (int i = 0; i < ship.load.Length; i++)
        {
            if (ship.load[i].HasValue)
                Instantiate(ship.load[i].Value.prefab, transform.GetChild(i));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(target, 3);
    }

    private void Update()
    {
        if (initialized)
        {
            if(Vector3.Distance(transform.position, target) > 2f)
                transform.Translate(transform.right * Time.deltaTime * -10);

        }

    }
}
