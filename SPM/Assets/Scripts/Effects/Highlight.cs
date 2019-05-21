using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    private bool isHighlighted = false;
    private MeshRenderer meshRenderer;
    private Color color;
    private Texture texture;
    private float metallic;
    private float smoothness;
    private Color emissionColor;

    [SerializeField] private Material regularMaterial;
    [SerializeField] private Material highlightedMaterial;

    private void Awake ()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        color = meshRenderer.material.color;
        texture = meshRenderer.material.GetTexture("_MainTex");
        metallic = meshRenderer.material.GetFloat("_Metallic");
        smoothness = meshRenderer.material.GetFloat("_Glossiness");
        emissionColor = meshRenderer.material.GetColor("_EmissionColor");
    }

    public void Activate ()
    {
        if (isHighlighted == false) {
            isHighlighted = true;
            highlightedMaterial.color = color;
            highlightedMaterial.SetTexture("_MainTex", texture);
            highlightedMaterial.SetFloat("_Metallic", metallic);
            highlightedMaterial.SetFloat("_Glossiness", smoothness);
            highlightedMaterial.SetColor("_EmissionColor", emissionColor);
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
