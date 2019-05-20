//Author: Marcus Mellström

using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private Transform respawnPoint;

    private void Start()
    {
        respawnPoint = GetComponent<Transform>().GetChild(0); 
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance != null) {
            GameManager.Instance.CurrentCheckPoint = respawnPoint.position;
        }
    }
}
