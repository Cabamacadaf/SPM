//Author: Marcus Mellström

using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteTrigger : MonoBehaviour
{
    [SerializeField] private int levelToLoad;
    [SerializeField] private LoadScene loadScene;

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player")) {
            loadScene.Load(levelToLoad);
        }
    }
}
