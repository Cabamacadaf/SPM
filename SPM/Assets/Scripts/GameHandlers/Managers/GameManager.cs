//Author: Simon Sundström
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private KeyCode RestartSceneButton = KeyCode.P;
    [SerializeField] private KeyCode RestartFromLatestCheckpointButton = KeyCode.O;
    private GameObject player;
  
    public Vector3 CurrentCheckPoint { get; set; }
    public bool RestartedFromLatestCheckpoint { get; set; }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
        RestartedFromLatestCheckpoint = false;
    }


    private void RestartSceneFromLatestCheckpoint()
    {
        RestartedFromLatestCheckpoint = true;
    }

    public void RespawnPlayer()
    {
        player.transform.position = CurrentCheckPoint;
        //ResetScene();
        //RestartedFromLatestCheckpoint = true;
    }

    public void ResetScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void RespawnPlayerNoReset()
    {
        player.transform.position = CurrentCheckPoint;
        player.GetComponent<HealthComponent>().Health = 100;
    }
}
