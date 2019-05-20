using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    private bool isHighlighted = false;
    private MeshRenderer meshRenderer;

    [SerializeField] private Material regularMaterial;
    [SerializeField] private Material highlightedMaterial;

    private void Awake ()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        Color color = meshRenderer.material.color;
        Texture texture = meshRenderer.material.GetTexture("_MainTex");
        float metallic = meshRenderer.material.GetFloat("_Metallic");
        float smoothness = meshRenderer.material.GetFloat("_Glossiness");
        highlightedMaterial.color = color;
        highlightedMaterial.SetTexture("_MainTex", texture);
        highlightedMaterial.SetFloat("_Metallic", metallic);
        highlightedMaterial.SetFloat("_Glossiness", smoothness);
    }

    public void Activate ()
    {
        if (isHighlighted == false) {
            isHighlighted = true;
            meshRenderer.material = highlightedMaterial;
        }
    }

    public void Deactivate ()
    {
        if (isHighlighted == true) {
            isHighlighted = false;
            meshRenderer.material = regularMaterial;
        }
    }
}
