using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{


    private void Start()
    {
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player")) {
            other.GetComponent<Player>().respawnPoint = GetComponentInChildren<Transform>();
            SceneController.Instance.lastCheckPointPos = GetComponentInChildren<Transform>().position;

        }
    }
}
