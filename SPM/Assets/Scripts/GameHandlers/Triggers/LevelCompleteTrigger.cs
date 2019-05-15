//Author: Marcus Mellström

using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteTrigger : MonoBehaviour
{
    [SerializeField] private int levelToLoad;
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player")) {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
