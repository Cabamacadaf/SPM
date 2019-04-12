using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Player player;
    private Enemy enemy;
    public bool hasAttacked;

    private void Awake ()
    {
        player = FindObjectOfType<Player>();
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter (Collider other)
    {
        if (enemy.attacking && other.CompareTag("Player") && !hasAttacked) {
            hasAttacked = true;
            player.Damage(enemy.attackDamage);
        }
    }
}
