using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    public KeyCode RestartSceneButton = KeyCode.P;
    public KeyCode RestartFromLatestCheckpointButton = KeyCode.O;
    public Vector3 lastCheckPointPos;

    public bool RestartedFromLatestCheckpoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(RestartSceneButton))
        {
            RestartScene();
        }
        if (Input.GetKeyDown(RestartFromLatestCheckpointButton))
        {
            RestartSceneFromLatestCheckpoint();
        }
    }

    private void RestartScene()
    {
        Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
        RestartedFromLatestCheckpoint = false;
    }


    private void RestartSceneFromLatestCheckpoint()
    {
        Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
        RestartedFromLatestCheckpoint = true;
    }
}
