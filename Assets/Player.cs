using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AICreatures;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    public float reviveRange;
    public float reviveTime;
    
    void Update()
    {
        controller.SimpleMove(new Vector3(speed*Game.GetInput().x,0, speed* Game.GetInput().y));
    }
    public void SpawnSkeleton(Human deadHuman)
    {
        Instantiate(Game.GetSkellyFab(), deadHuman.transform.position, deadHuman.transform.rotation);
    }
}
