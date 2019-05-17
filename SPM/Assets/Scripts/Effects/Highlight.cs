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
    }

    public void Activate ()
    {
        if (isHighlighted == false) {
            isHighlighted = true;
            Color color = meshRenderer.material.color;
            meshRenderer.material = highlightedMaterial;
            meshRenderer.material.color = color;
        }
    }

    public void Deactivate ()
    {
        if (isHighlighted == true) {
            isHighlighted = false;
            Color color = meshRenderer.material.color;
            meshRenderer.material = regularMaterial;
            meshRenderer.material.color = color;
        }
    }
}
