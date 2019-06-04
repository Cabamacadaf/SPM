//Author: Marcus Mellström

using UnityEngine;

public class Attack : MonoBehaviour
{
    private HealthComponent playerHealth;
    private Enemy enemy;
    private float damage;

    private void Awake ()
    {
        enemy = GetComponentInParent<Enemy>();
        if(gameObject.name == "Leap Attack Hitbox") {
            Enemy2 enemy2 = (Enemy2)enemy;
            damage = enemy2.LeapDamage;
        }
        else if(gameObject.name == "Attack Mesh"){
            damage = enemy.AttackDamage;
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player") && enemy.HasAttacked == false) {
            enemy.HasAttacked = true;
            playerHealth = other.GetComponent<HealthComponent>();
            playerHealth.Damage(damage);
        }
    }
}
