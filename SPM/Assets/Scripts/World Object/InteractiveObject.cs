using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    void Update()
    {
        if (GetComponent<MeshRenderer>().isVisible) {
            Debug.Log("Button Visible");
        }
    }
}
