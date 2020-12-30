using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        controller.SimpleMove(new Vector3(-speed*Input.GetAxis("Vertical"),0, speed*Input.GetAxis("Horizontal")));
    }
}
