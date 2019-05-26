//Main Author: Simon Sundström
//Secondary Author: Marcus Mellström
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public static Player PlayerInstance { get; private set; }
    [SerializeField] private Player playerInstance;

    public static Canvas CanvasInstance { get; private set; }
    [SerializeField] private Canvas canvasInstance;

    [SerializeField] private KeyCode RestartSceneButton = KeyCode.P;
    [SerializeField] private KeyCode RestartFromLatestCheckpointButton = KeyCode.O;
    [SerializeField] private KeyCode nextLevelButton = KeyCode.L;
    private GameObject player;
  
    public Vector3 CurrentCheckPoint { get; set; }
    public bool RestartedFromLatestCheckpoint { get; set; }

    private void Awake()
    {
        PlayerInstance = FindObjectOfType<Player>();
        CanvasInstance = FindObjectOfType<Canvas>();

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
        if (Input.GetKeyDown(nextLevelButton)) {
            StartNextLevel();
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
        //player.GetComponent<HealthComponent>().Health = 100;
    }

    private void StartNextLevel ()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1){
            SceneManager.LoadScene(2);
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2){
            SceneManager.LoadScene(0);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 0) {
            SceneManager.LoadScene(1);
        }
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
}
