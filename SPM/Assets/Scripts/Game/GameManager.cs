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
    [SerializeField] private Light Flashlight;


    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private KeyCode RestartSceneButton = KeyCode.P;
    [SerializeField] private KeyCode RestartFromLatestCheckpointButton = KeyCode.O;
    [SerializeField] private KeyCode nextLevelButton = KeyCode.L;
    private GameObject player;
    private Camera mainCamera;

    public int powercores { get; set; }
    public Vector3 CurrentCheckPoint { get; set; }
    public bool RestartedFromLatestCheckpoint { get; set; }
    public bool HasFlashlight { get; set; }

    private void Awake()
    {
        SetOnAwake();

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
            Pause();
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
   
        PlayerPrefs.SetFloat("PlayerPositionX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPositionY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", player.transform.position.z);
        PlayerPrefs.SetFloat("PlayerRotationX", player.transform.rotation.eulerAngles.x);
        PlayerPrefs.SetFloat("PlayerRotationY", player.transform.rotation.eulerAngles.y);
        PlayerPrefs.SetFloat("PlayerRotationZ", player.transform.rotation.eulerAngles.z);

        PlayerPrefs.SetFloat("CameraRotationX", mainCamera.transform.rotation.eulerAngles.x);
        PlayerPrefs.SetFloat("CameraRotationY", mainCamera.transform.rotation.eulerAngles.y);
        PlayerPrefs.SetFloat("CameraRotationZ", mainCamera.transform.rotation.eulerAngles.z);

        PlayerPrefs.SetFloat("PlayerHealth: ", playe)

        if(HasFlashlight == true)
        {
            PlayerPrefs.SetInt("Flashlight", 1);
        }

        PlayerPrefs.SetInt("PowerCores", GameController.instance.PowerCoreCounter);

        PlayerPrefs.Save();
        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {

        player.transform.position = CurrentCheckPoint;
        player.transform.rotation = Quaternion.Euler(PlayerPrefs.GetFloat("PlayerRotationX"), PlayerPrefs.GetFloat("PlayerRotationY"), PlayerPrefs.GetFloat("PlayerRotationZ"));

        mainCamera.transform.rotation = Quaternion.Euler(PlayerPrefs.GetFloat("CameraRotationX"), PlayerPrefs.GetFloat("CameraRotationY"), PlayerPrefs.GetFloat("CameraRotationZ"));


        CameraController.instance.rotationX = player.transform.eulerAngles.x;
        CameraController.instance.rotationY = player.transform.eulerAngles.y;

        GameController.instance.PowerCoreCounter = PlayerPrefs.GetInt("PowerCores");

        if(PlayerPrefs.GetInt("Flashlight") == 1)
        {
            HasFlashlight = true;
            if(PlayerPrefs.GetInt("FlashlightEnabled") == 1)
            {
                Flashlight.enabled = true;

            }
            else
            {
                Flashlight.enabled = false;
            }
        }
        else
        {
            HasFlashlight = false;
        }

        Debug.Log("Game Loaded");

    }
    #endregion

    #region UI

    public void Pause()
    {
        CameraController.instance.UnLockCursor();

        pauseMenu.SetActive(true);

        Time.timeScale = 0;
    }
    public void Continue()
    {
        Time.timeScale = 1;
        CameraController.instance.LockCursor();
        
        pauseMenu.SetActive(false);


    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    #endregion

    #region Key Items
    private void HandleFlashLight()
    {
        if (HasFlashlight)
        {
            if (Input.GetKeyDown(KeyCode.F) && Flashlight.enabled == false){
                Flashlight.enabled = true;
                PlayerPrefs.SetInt("FlashlightEnabled", 1);
            }
            else if(Input.GetKeyDown(KeyCode.F) && Flashlight.enabled)
            {
                Flashlight.enabled = false;
                PlayerPrefs.SetInt("FlashlightEnabled", 0);
            }
        }
    }
    #endregion
}
