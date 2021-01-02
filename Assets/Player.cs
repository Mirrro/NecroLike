using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AICreatures;

public class Player : MonoBehaviour, ITargetable
{
    public CharacterController controller;
    public float speed;
    public float reviveRange;
    public float reviveTime;
    public float tetherDistance;
    public int health;
    
    void Update()
    {
        controller.SimpleMove(new Vector3(speed*Game.GetInput().x,0, speed* Game.GetInput().y));
    }
    public void SpawnSkeleton(AICreature deadHuman)
    {
        Instantiate(Game.GetSkellyFab(), deadHuman.transform.position, deadHuman.transform.rotation);
    }

    public void GetHit(int damage)
    {
        health -= damage;
        if (health <= 0)
            Death();
    }

    public void Death()
    {
        Application.Quit();
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
