//Author: Marcus Mellström

using UnityEngine;

public class Attack : MonoBehaviour
{
    private HealthComponent playerHealth;
    private Enemy enemy;
    [HideInInspector] public bool hasAttacked;

    private void Awake ()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player") && !hasAttacked) {
            hasAttacked = true;
            playerHealth = other.GetComponent<HealthComponent>();
            playerHealth.Damage(enemy.attackDamage);
        }
    }
}
