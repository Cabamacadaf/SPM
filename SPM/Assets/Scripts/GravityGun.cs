using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
    [SerializeField] private float pushRange;
    [SerializeField] private float pullRange;
    [SerializeField] private float pushForce;
    [SerializeField] private float pullForce;
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform pullPoint;
    
    public void Push ()
    {
        Debug.Log("Push");
        if(Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, pushRange, hitLayer)) {
            Debug.Log("Hit");
            Debug.DrawLine(firePoint.position, hit.point, Color.red, 2);
            if (hit.collider.attachedRigidbody != null) {
                Debug.Log(1-(hit.distance / pushRange));
                hit.collider.attachedRigidbody.AddForce(firePoint.forward * pushForce * (1-(hit.distance / pushRange)));
            }
        }
        else {
            Debug.DrawRay(firePoint.position, firePoint.forward * pushRange, Color.blue, 2);
        }
    }

    public void Pull ()
    {
        Debug.Log("Pull");
        if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, pullRange, hitLayer)) {
            Debug.Log("Hit");
            Debug.DrawLine(firePoint.position, hit.point, Color.green, 2);

            hit.collider.GetComponent<PickUpObject>().Pull(pullForce, pullPoint);
            //if (hit.collider.attachedRigidbody != null) {
            //    hit.collider.attachedRigidbody.AddForce(-firePoint.forward * pullForce * (1-(hit.distance / pullRange)));
            //}
        }
        else {
            Debug.DrawRay(firePoint.position, firePoint.forward * pushRange, Color.blue, 2);
        }
    }
}
