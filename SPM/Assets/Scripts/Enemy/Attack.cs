//Author: Marcus Mellström

using UnityEngine;

public class Attack : MonoBehaviour
{
    private Player player;
    private Enemy enemy;
    [HideInInspector] public bool hasAttacked;

    private void Awake ()
    {
        player = FindObjectOfType<Player>();
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player") && !hasAttacked) {
            hasAttacked = true;
            player.Damage(enemy.attackDamage);
        }
    }
}
