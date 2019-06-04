using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;

    public GameObject LoadingScreen { get => loadingScreen; set => loadingScreen = value; }

    public void Load(int sceneToLoad)
    {
        Time.timeScale = 0;
        loadingScreen.SetActive(true);

        StartCoroutine(LoadNewScene(sceneToLoad));
    }

    private IEnumerator LoadNewScene (int sceneToLoad)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);
        
        while (async.isDone == false) {
            yield return null;
        }
    }
}
