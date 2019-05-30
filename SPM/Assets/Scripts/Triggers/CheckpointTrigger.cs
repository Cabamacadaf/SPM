//Author: Marcus Mellström

using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private Transform respawnPoint;
    private bool reachedCheckpoint;

    private void Start()
    {
        respawnPoint = GetComponent<Transform>().GetChild(0); 
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance != null && reachedCheckpoint == false) {
            GameManager.Instance.CurrentCheckPoint = respawnPoint.position;
            GameManager.Instance.SaveGame();
            reachedCheckpoint = true;
        }
    }
}
