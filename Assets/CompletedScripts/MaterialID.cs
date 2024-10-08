using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialID : MonoBehaviour
{
    public Renderer objectRenderer;
    public float newAlpha =0.5f;

    void Start()
    {
        Material material = objectRenderer.material;

        material.SetFloat("_Mode", 3);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;

        Color color = material.color;
        color.a = newAlpha;
        material.color = color;
    }

}
