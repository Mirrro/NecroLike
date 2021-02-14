using AIUnits;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
        Debug.Log(ship.load.Length);
        for (int i = 0; i < ship.load.Length; i++)
        {
            Debug.Log("spawned" + ship.load[i].prefab.name);
            Instantiate(ship.load[i].prefab, transform.GetChild(i));
        }
    }
    
    public void PositionUnits(Ship ship, bool isSwappable)
    {
        if (!isSwappable)
        {
            PositionUnits(ship);
            return;
        }
        Debug.Log(ship.load.Length);
        for (int i = 0; i < ship.load.Length; i++)
        {
            Debug.Log("spawned" + ship.load[i].prefab.name);
            var child = transform.GetChild(i);
            var unit = Instantiate(ship.load[i].prefab, transform.GetChild(i));
            unit.GetComponent<AIEntity>().enabled = false;
            child.gameObject.GetComponent<CapsuleCollider>().enabled = true;
            var swapperSelectable = child.gameObject.AddComponent<UnitSwapperSelectable>();
            swapperSelectable.unit = ship.load[i];
        }
    }

    public void UpdateUnits(Ship ship)
    {
        for (int i = 0; i < ship.load.Length; i++)
        {
            var child = transform.GetChild(i);
            for (int j = 0; j < child.childCount; j++)
            {
                Destroy(child.GetChild(i));
            }
            var unit = Instantiate(ship.load[i].prefab, transform.GetChild(i));
            unit.GetComponent<AIEntity>().enabled = false;
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
