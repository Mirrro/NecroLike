using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderBehaviour : MonoBehaviour,  ILevelStateListener
{
    [SerializeField]
    private ShaderHandler blueprintShader;
    [SerializeField]
    private ShaderHandler pingShader;



    public IEnumerator TransitionToBlueprint()
    {
        blueprintShader.Set();
        for (float t = 0f; t < 1; t += Time.deltaTime * Level.TransitionSpeed)
        { 
            blueprintShader.ShaderLerpRadius(t);
            yield return null;
        }
    }
    public IEnumerator TransitionToFight()
    {
        for (float t = 1f; t > 0; t -= Time.deltaTime * Level.TransitionSpeed)
        {
            blueprintShader.ShaderLerpRadius(t);
            yield return null;
        }
        pingShader.Set();
    }

    public IEnumerator Ping()
    {
        for (float t = 1f; t > 0; t -= Time.deltaTime * Level.TransitionSpeed)
        {
            pingShader.ShaderLerpRadius(t - t * t);
            yield return null;
        }
    }

    public void OnStateEnd(Level.State state)
    {
        throw new System.NotImplementedException();
    }

    public void OnStateBegin(Level.State state)
    {
        throw new System.NotImplementedException();
    }
}
