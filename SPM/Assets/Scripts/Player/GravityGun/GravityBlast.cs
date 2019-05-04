using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBlast : MonoBehaviour
{
    public Collider blastRadius;
    public Renderer meshRenderer;

    public float blastForce = 50000.0f;

    public void Blast ()
    {
        StartCoroutine(EnableTrigger());
    }

    IEnumerator EnableTrigger ()
    {
        meshRenderer.enabled = true;
        blastRadius.enabled = true;
        yield return 0;
        meshRenderer.enabled = false;
        blastRadius.enabled = false;
    }
}
