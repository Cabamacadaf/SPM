using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBlast : MonoBehaviour
{
    public Collider blastRadius;

    public void Blast ()
    {
        blastRadius.enabled = true;
        Debug.Log("Blast");
    }
}
