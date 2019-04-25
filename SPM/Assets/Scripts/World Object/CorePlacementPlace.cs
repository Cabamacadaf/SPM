using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorePlacementPlace : MonoBehaviour
{
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("PowerCore")) {
            other.transform.parent = transform;
        }
    }
}
