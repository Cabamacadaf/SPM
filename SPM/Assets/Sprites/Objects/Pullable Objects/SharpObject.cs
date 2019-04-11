using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpObject : PickUpObject
{


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<Enemy>().Damage(rb.velocity.magnitude, damage);
            durability--;
            if (durability == 0)
            {
                Destroy(this.gameObject);
            }
        }


    }
}
