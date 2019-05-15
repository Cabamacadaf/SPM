using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillzoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerDeathEvent deathEvent = new PlayerDeathEvent(GameManager.Instance.lastCheckPointPos);
            deathEvent.ExecuteEvent();
        }
    }
}
