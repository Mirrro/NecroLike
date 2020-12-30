using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AICreatures;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    void Update()
    {
        controller.SimpleMove(new Vector3(-speed*Input.GetAxis("Vertical"),0, speed*Input.GetAxis("Horizontal")));
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Skeleton")
            other.GetComponent<Skeleton>().forceFollowPlayer = false;
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Skeleton")
            other.GetComponent<Skeleton>().forceFollowPlayer = true;
    }
}
