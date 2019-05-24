//Author: Marcus Mellström

using UnityEngine;

public class Attack : MonoBehaviour
{
    private HealthComponent playerHealth;
    private Enemy enemy;
    public bool HasAttacked { get; set; }

    private void Awake ()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player") && HasAttacked == false) {
            HasAttacked = true;
            playerHealth = other.GetComponent<HealthComponent>();
            playerHealth.Damage(enemy.AttackDamage);
        }
    }
}
