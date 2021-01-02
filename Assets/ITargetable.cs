using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetable
{
    Vector3 GetPosition();
    
    void GetHit(int damage);

    void Death();

    bool IsAlive();
}
