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
        print("pingshader set");
        pingShader.Set();
    }

    public IEnumerator PingAnimation()
    {
        for (float t = 1f; t > 0; t -= Time.deltaTime * Level.TransitionSpeed)
        {
            pingShader.ShaderLerpRadius(t - t * t);
            yield return null;
        }
    }

    public void Ping(Vector3 position)
    {
        pingShader.SetPosition(position);
        StartCoroutine(PingAnimation());
    }

    private void Start()
    {
        Game.level.rallyEvent.AddListener(Ping);    
    }

    public void OnStateEnd(Level.State state)
    {
        if (state == Level.State.Entry)
            StartCoroutine(TransitionToBlueprint());
        else if (state == Level.State.Positioning)
            StartCoroutine(TransitionToFight());
    }

    public void OnStateBegin(Level.State state)
    {
    }
}
