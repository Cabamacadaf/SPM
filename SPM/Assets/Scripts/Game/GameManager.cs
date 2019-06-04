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
    public static CameraController CameraInstance { get; private set; }

    public Vector3 CurrentCheckPoint { get; set; }
    public bool RestartedFromLatestCheckpoint { get; set; }
    public bool HasFlashlight { get; set; }
    public bool HasSavedFile { get; set; }
    public bool HasKeycard { get; set; }

    public bool GameWasLoaded { get; set; }
    public bool GameIsPaused { get; set; }

    public int CurrentSceneIndex { get; set; }

    private LoadScene loadScene;

    private GameObject loadingScreen;
    private GameObject player;
    private GameObject mainCamera;

    private void Awake ()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else {
            Destroy(gameObject);
        }

        loadScene = GetComponent<LoadScene>();
        SetOnAwake();
    }


    #region Setter/Getters 
    public void SetPlayer (GameObject player)
    {
        this.player = player;
    }

    public void SetCamera (GameObject camera)
    {
        mainCamera = camera;
    }

    public void SetLoadingScreen (GameObject loadingScreen)
    {
        loadScene.LoadingScreen = loadingScreen;
    }

    private void SetOnAwake ()
    {
        PlayerInstance = FindObjectOfType<Player>();
        CanvasInstance = FindObjectOfType<Canvas>();
        CameraInstance = FindObjectOfType<CameraController>();
    }

    #endregion

    #region  SceneManagement
    private void RestartScene ()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
        RestartedFromLatestCheckpoint = false;
    }


    private void RestartSceneFromLatestCheckpoint ()
    {
        RestartedFromLatestCheckpoint = true;
    }

    public void RespawnPlayer ()
    {
        LoadGame();
    }

    public void ResetScene ()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void RespawnPlayerNoReset ()
    {
        player.transform.position = CurrentCheckPoint;
    }

    private void StartNextLevel ()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            SceneManager.LoadScene(2);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2) {
            SceneManager.LoadScene(0);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 0) {
            SceneManager.LoadScene(1);
        }
    }
    #endregion

    #region Save
    public void SaveGame ()
    {
        PlayerInstance.SavePlayer();
        LevelManager.Instance.Save();

        PlayerPrefs.SetInt("SavedGame", 1);
        Scene currentScene = SceneManager.GetActiveScene();
        PlayerPrefs.SetInt("CurrentLevel", currentScene.buildIndex);

        if (HasFlashlight && PlayerPrefs.GetInt("Flashlight") == 0) {
            PlayerPrefs.SetInt("Flashlight", 1);
        }
        if (HasKeycard && PlayerPrefs.GetInt("Keycard") == 0) {
            PlayerPrefs.SetInt("Keycard", 1);
        }

        Debug.Log("Game Saved");

    }



    public void LoadGame ()
    {
        loadScene.Load(SceneManager.GetActiveScene().buildIndex);
        PlayerInstance.LoadPlayer();
        SaveSystem.LoadObjects();
        SaveSystem.LoadEnemies();
        SaveSystem.LoadDestructibleObjects();
        SaveSystem.LoadHealthPacks();
    }

    public void NewGame ()
    {
        PlayerPrefs.SetInt("SavedGame", 0);
        PlayerPrefs.SetInt("Flashlight", 0);
        HasFlashlight = false;
        PlayerPrefs.SetInt("Keycard", 0);
        HasKeycard = false;
        SaveSystem.DeleteFile();
        HasSavedFile = false;
        GameIsPaused = false;

    }

    #endregion
}
