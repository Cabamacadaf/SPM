using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapAttack : MonoBehaviour
{
    private Player player;
    private Enemy2 enemy;
    [HideInInspector] public bool hasAttacked;

    private void Awake ()
    {
        player = FindObjectOfType<Player>();
        enemy = GetComponentInParent<Enemy2>();
    }
    private void OnTriggerEnter (Collider other)
    {
        if (enemy.attacking && other.CompareTag("Player") && !hasAttacked) {
            hasAttacked = true;
            player.Damage(enemy.attackDamage);
        }
    }
}
