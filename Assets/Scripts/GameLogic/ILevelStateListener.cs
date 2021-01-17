using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelStateListener
{
    void OnLevelStateEnd(Level.State state);
    void OnLevelStateBegin(Level.State state);
}
