using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillzoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided");
            DeathEvent deathEvent = new DeathEvent(other.gameObject);
            deathEvent.ExecuteEvent();
        }
    }
}
