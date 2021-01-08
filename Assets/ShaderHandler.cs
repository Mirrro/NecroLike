using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShaderHandler : ScriptableObject
{

    [MenuItem("Assets/Create/ShaderHandler")]
    public static void CreateShaderHandler()
    {
        ShaderHandler asset = CreateInstance<ShaderHandler>();

        AssetDatabase.CreateAsset(asset, "Assets/ShaderHandler.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    [SerializeField] private Material shader;

    private static readonly int POS = Shader.PropertyToID("Vector3_4251960c556e4888853e72ddd90e10ff");
    private static readonly int RADIUS = Shader.PropertyToID("Vector1_8ca1ba16de1c45deb4e0cd5b6477bc66");
    private static readonly int THICC = Shader.PropertyToID("Vector1_8da350d839b24a3b93ac767f2cb80754");
    private static readonly int HARD = Shader.PropertyToID("Vector1_bdac1cb1751e43dfb339c047c0f7e819");

    private static readonly int TEX = Shader.PropertyToID("Texture2D_965f33e760434db59f97739d1cb82dcc");//Texture2D_00b90a2dcebb4aacbcdc115a47b2bdd7
    private static readonly int COLOR_1 = Shader.PropertyToID("Color_48695a667130496ea5e65afe199deb29");
    private static readonly int COLOR_2 = Shader.PropertyToID("Color_1b26e2bdb08c4076b413f7087791d82d");
    
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float startRadius;
    [SerializeField] private float maxRadius;
    [SerializeField] private float startThickness;
    [SerializeField] private float startHardness;

    [SerializeField] private Texture startTexture;
    [SerializeField] private Color startColor1;
    [SerializeField] private Color startColor2;
    
    public void Set()
    {
        SetPosition(startPosition);
        SetRadius(startRadius);
        SetThickness(startThickness);
        SetHardness(startHardness);

        SetTexture(startTexture);
        SetColor1(startColor1);
        SetColor2(startColor2);
    } 
    
    public void SetPosition(Vector3 position)
    {
        shader.SetVector(POS, position);
    }
    public void SetRadius(float radius)
    {
        shader.SetFloat(RADIUS, radius);
    }
    public void SetThickness(float thickness)
    {
        shader.SetFloat(THICC, thickness);
    }
    public void SetHardness(float hardness)
    {
        shader.SetFloat(HARD, hardness);
    }

    public void SetTexture(Texture texture)
    {
        shader.SetTexture(TEX, texture);
    }
    public void SetColor1(Color color)
    {
        shader.SetColor(COLOR_1, color);
    }
    public void SetColor2(Color color)
    {
        shader.SetColor(COLOR_2, color);
    }

    public void ShaderLerpRadius(float t)
    {
        SetRadius(startRadius + (t * maxRadius));
    }

}
