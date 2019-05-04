using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastRadius : MonoBehaviour
{
    GravityBlast gravityBlast;
    private void Awake ()
    {
        gravityBlast = GetComponentInParent<GravityBlast>();
    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Enemy")) {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.Transition<EnemyBlastedState>();
            enemy.rigidBody.AddForce((enemy.transform.position - enemy.player.transform.position).normalized * gravityBlast.blastForce);
        }
    }
}
