//Author: Marcus Mellström

using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player")) {
            other.GetComponent<Player>().respawnPoint = GetComponentInChildren<Transform>();
            SceneController.Instance.lastCheckPointPos = GetComponentInChildren<Transform>().position;
        }
    }
}
