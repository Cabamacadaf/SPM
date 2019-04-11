using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObject : PickUpObject
{
    [SerializeField] private float radius;
    private void OnCollisionEnter(Collision collision)
    {
        var colls = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider col in colls)
        {
            Debug.Log("Explosion");

            if (col.gameObject.tag == "Player")
            {
                Debug.Log("Player Hit");
            }
        }
    }
}
