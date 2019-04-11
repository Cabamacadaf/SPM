using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObject : PickUpObject
{
    [SerializeField] private float radius;
    private void OnCollisionEnter(Collision collision)
    {
        if (active || thrown)
        {
            Collider[] colls = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider col in colls)
            {
                Vector3 hit;
                if (col.gameObject.CompareTag("Player"))
                {
                    hit = col.ClosestPoint(transform.position);

                    Debug.Log(col.ClosestPoint(transform.position));
                    Player player = col.gameObject.GetComponent<Player>();
                    player.Damage(damage - hit.x - hit.z);
                }
                if (col.gameObject.CompareTag("Enemy"))
                {
                    hit = col.ClosestPoint(transform.position);
                    
                    Enemy enemy = col.gameObject.GetComponent<Enemy>();
                    enemy.Damage(damage- hit.x - hit.z);
                }
            }
            LoseDurability();
        }
  
    }
}
