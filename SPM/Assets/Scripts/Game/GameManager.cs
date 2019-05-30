//Main Author: Simon Sundström
//Secondary Author: Marcus Mellström
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public static Player PlayerInstance { get; private set; }
    public static Canvas CanvasInstance { get; private set; }

    //[SerializeField] private List<GameObject> powerCores;
    //[SerializeField] private List<GameObject> powerSourcePlaces;

    //[SerializeField] private GameObject keyCard;
    //[SerializeField] private Transform keySpawnPoint;

    public bool HasLevel1Keycard { get; set; }
    public bool HasLevel2Keycard { get; set; }
    public bool HasAllPowerCores { get; private set; }
    public int PowerCoreCounter { get; set; }
    [SerializeField] private KeyCode RestartSceneButton = KeyCode.P;
    [SerializeField] private KeyCode RestartFromLatestCheckpointButton = KeyCode.O;
    [SerializeField] private KeyCode nextLevelButton = KeyCode.L;
    private GameObject player;
    private Camera mainCamera;

    public Vector3 CurrentCheckPoint { get; set; }
    public bool RestartedFromLatestCheckpoint { get; set; }
    public bool HasFlashlight { get; set; }

    private void Awake()
    {
        SetOnAwake();
        //LoadGame();
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
        HandleInput();

    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(RestartSceneButton))
        {
            RestartScene();
        }
        if (Input.GetKeyDown(RestartFromLatestCheckpointButton))
        {
            RestartSceneFromLatestCheckpoint();
        }
        if (Input.GetKeyDown(nextLevelButton))
        {
            StartNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIController.instance.Pause();
        }
    }


    #region Setter/Getters 
    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }

    private void SetOnAwake()
    {
        PlayerInstance = FindObjectOfType<Player>();
        CanvasInstance = FindObjectOfType<Canvas>();


        mainCamera = Camera.main;
    }

    #endregion

    #region  SceneManagement
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
        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.buildIndex);
        LoadGame();
    }

    public void ResetScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void RespawnPlayerNoReset()
    {
        player.transform.position = CurrentCheckPoint;
    }

    private void StartNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(2);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene(0);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }
    }
    #endregion

    #region Save
    public void SaveGame()
    {
        SavePlayer();

        SaveObjectives();

        PlayerPrefs.Save();
        Debug.Log("Game Saved");
    }

    private void SaveObjectives()
    {
        if(HasFlashlight)
        {
            PlayerPrefs.SetInt("Flashlight", 1);
        }

        PlayerPrefs.SetInt("PowerCores", PowerCoreCounter);
    }

    private void SavePlayer()
    {
        PlayerPrefs.SetFloat("PlayerPositionX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPositionY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", player.transform.position.z);
        PlayerPrefs.SetFloat("PlayerRotationX", player.transform.rotation.eulerAngles.x);
        PlayerPrefs.SetFloat("PlayerRotationY", player.transform.rotation.eulerAngles.y);
        PlayerPrefs.SetFloat("PlayerRotationZ", player.transform.rotation.eulerAngles.z);

        Debug.Log("PlayerPosition: " + new Vector3(PlayerPrefs.GetFloat("PlayerPositionX"), PlayerPrefs.GetFloat("PlayerPositionY", PlayerPrefs.GetFloat("PlayerPositionZ"))));

        PlayerPrefs.SetFloat("CameraRotationX", mainCamera.transform.rotation.eulerAngles.x);
        PlayerPrefs.SetFloat("CameraRotationY", mainCamera.transform.rotation.eulerAngles.y);
        PlayerPrefs.SetFloat("CameraRotationZ", mainCamera.transform.rotation.eulerAngles.z);

        PlayerPrefs.SetFloat("PlayerHealth", PlayerInstance.PlayerHealth.Health);
    }

    public void LoadGame()
    {
        LoadPlayer();

        LoadObjectives();

        Debug.Log("Game Loaded");

    }

    private void LoadObjectives()
    {
        PowerCoreCounter = PlayerPrefs.GetInt("PowerCores");

        if (PlayerPrefs.GetInt("Flashlight") == 1)
        {
            HasFlashlight = true;

        }
        else
        {
            HasFlashlight = false;
        }
    }

    private void LoadPlayer()
    {
        //player.transform.position = CurrentCheckPoint;
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerPositionX"), PlayerPrefs.GetFloat("PlayerPositionY"), PlayerPrefs.GetFloat("PlayerPositionZ"));

        player.transform.rotation = Quaternion.Euler(PlayerPrefs.GetFloat("PlayerRotationX"), PlayerPrefs.GetFloat("PlayerRotationY"), PlayerPrefs.GetFloat("PlayerRotationZ"));

        mainCamera.transform.rotation = Quaternion.Euler(PlayerPrefs.GetFloat("CameraRotationX"), PlayerPrefs.GetFloat("CameraRotationY"), PlayerPrefs.GetFloat("CameraRotationZ"));

        CameraController.instance.rotationX = player.transform.eulerAngles.x;
        CameraController.instance.rotationY = player.transform.eulerAngles.y;

        PlayerInstance.PlayerHealth.Health = PlayerPrefs.GetFloat("PlayerHealth");
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
    }

    #endregion




}
