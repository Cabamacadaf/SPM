using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObject : PickUpObject
{
    public GameObject explosion1;
    public GameObject explosion2;
    public float power = 10.0F;

    [SerializeField] private float explosionDamage = 101f;
    [SerializeField] private float radius;
    private void OnCollisionEnter (Collision collision)
    {
        Debug.Log("Velocity: " + rb.velocity.magnitude);
        if (thrown && rb.velocity.magnitude > lowestVelocityToDoDamage) {
            Collider[] colls = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider col in colls) {
                Vector3 explosionPos = transform.position;
                Rigidbody rb = col.GetComponent<Rigidbody>();
                if (rb != null) {
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0f);
                }

                Vector3 hit;
                if (col.CompareTag("Player")) {
                    hit = col.ClosestPoint(transform.position);

                    Player player = col.gameObject.GetComponent<Player>();
                    player.Damage(explosionDamage - hit.x - hit.z);
                }

                if (col.CompareTag("Damageable")) {
                    hit = col.ClosestPoint(transform.position);

                    EnemyBaseState enemyState = (EnemyBaseState)col.GetComponent<Enemy>().GetCurrentState();

                    //enemyState.Damage(damage - hit.x - hit.z);
                    enemyState.Damage(explosionDamage);
                }

                if (col.CompareTag("Enemy2Hurtbox")) {
                    Enemy2 enemy = col.GetComponentInParent<Enemy2>();
                    EnemyBaseState enemyState = (EnemyBaseState)enemy.GetCurrentState();

                    enemyState.Damage(explosionDamage * enemy.damageReduction);
                }
            }
            Instantiate(explosion1, transform.position, Quaternion.identity);
            Instantiate(explosion2, transform.position, Quaternion.identity);

            LoseDurability();
        }
    }
}
