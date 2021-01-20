using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectionStateListener
{
    void OnSelectionState(InputHandler.SelectionState state);
}