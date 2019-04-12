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
        if (other.CompareTag("Player") && !hasAttacked) {
            hasAttacked = true;
            Debug.Log("Player hit");
            player.Damage(enemy.attackDamage);
        }
    }
}
