using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour, ILevelStateListener
{
    [SerializeField] protected Transform[] stateTransforms;

    public void OnLevelStateBegin(Level.State state)
    {
    }
    public void OnLevelStateEnd(Level.State state)
    {
        StartCoroutine(Transition(((int)state) + 1));
    }

    public IEnumerator Transition(int position)
    {
        for (float t = 0f; t < 1; t += Time.deltaTime * Level.TransitionSpeed)
        {
            transform.position = Vector3.Lerp(stateTransforms[position-1].position, stateTransforms[position].position, t);
            transform.rotation = Quaternion.Lerp(stateTransforms[position - 1].rotation, stateTransforms[position].rotation, t);
            yield return null;
        }
    }
}
