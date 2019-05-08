//Author: Marcus Mellström

using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteTrigger : MonoBehaviour
{
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player")) {
            SceneManager.LoadScene("Level2_Whitebox");
        }
    }
}
