//Author: Marcus Mellström

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectsTrigger : MonoBehaviour
{
    [SerializeField] private Rigidbody[] objects;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Rigidbody thing in objects) {
                thing.isKinematic = false;
                thing.gameObject.layer = LayerMask.NameToLayer("Intangible");
            }
        }
    }
}
