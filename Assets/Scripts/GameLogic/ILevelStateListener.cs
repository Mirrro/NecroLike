using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelStateListener
{
    void OnStateEnd(Level.State state);
    void OnStateBegin(Level.State state);
}
