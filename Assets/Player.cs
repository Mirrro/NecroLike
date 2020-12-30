using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AICreatures;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    public List<Skeleton> army = new List<Skeleton>();
    void Update()
    {
        controller.SimpleMove(new Vector3(-speed*Input.GetAxis("Vertical"),0, speed*Input.GetAxis("Horizontal")));
    }
    public void SpawnSkeleton(Human deadHuman)
    {
        GameObject newSkelly = Instantiate(Game.GetSkellyFab(), deadHuman.transform.position, deadHuman.transform.rotation);
        army.Add(newSkelly.GetComponent<Skeleton>());
    }
    public void OnSkeletonDeath(Skeleton skeleton)
    {
        army.Remove(skeleton);
    }
}
