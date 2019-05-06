using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillzoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.Respawn();
        }
    }
}
