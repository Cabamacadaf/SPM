using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    private bool isHighlighted = false;
    private MeshRenderer meshRenderer;
    private Color color;
    private float metallic;
    private float smoothness;

    [SerializeField] private Material regularMaterial;
    [SerializeField] private Material highlightedMaterial;

    private void Awake ()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Activate ()
    {
        if (isHighlighted == false) {
            isHighlighted = true;
            color = meshRenderer.material.color;
            metallic = meshRenderer.material.GetFloat("_Metallic");
            smoothness = meshRenderer.material.GetFloat("_Glossiness");
            meshRenderer.material = highlightedMaterial;
            meshRenderer.material.color = color;
            meshRenderer.material.SetFloat("_Metallic", metallic);
            meshRenderer.material.SetFloat("_Glossiness", smoothness);
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
