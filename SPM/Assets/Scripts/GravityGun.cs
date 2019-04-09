using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float force;
    [SerializeField] private LayerMask hitLayer;
   public void Shoot ()
    {
        Debug.Log("Shoot");
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.forward, out hit, range, hitLayer)) {
            Debug.Log("Hit");
            if (hit.collider.attachedRigidbody != null) {
                hit.collider.attachedRigidbody.AddForce(hit.normal * force);
            }
        }
        Debug.DrawRay(transform.position, Vector3.forward, Color.red, range);
    }
}
