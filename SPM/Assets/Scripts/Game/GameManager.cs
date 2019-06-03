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

    public int CurrentSceneIndex { get; set; }

    private GameObject player;
    private GameObject mainCamera;

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

        SetOnAwake();


        
    }


    #region Setter/Getters 
    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }

    public void SetCamera(GameObject camera)
    {
        mainCamera = camera;
    }

    private void SetOnAwake()
    {
        PlayerInstance = FindObjectOfType<Player>();
        CanvasInstance = FindObjectOfType<Canvas>();
        CameraInstance = FindObjectOfType<CameraController>();
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
        PlayerInstance.SavePlayer();
        //HasSavedFile = true;
        PlayerPrefs.SetInt("SavedGame", 1);

        if(HasFlashlight && PlayerPrefs.GetInt("Flashlight") == 0)
        {
            PlayerPrefs.SetInt("Flashlight", 1);
        }
        if (HasKeycard && PlayerPrefs.GetInt("Keycard") == 0)
        {
            PlayerPrefs.SetInt("Keycard", 1);
        }

        //ObjectsData save = LevelManager.instance.CreateSaveGameObject();

        //SaveSystem.SaveObjects(save);
        
        Debug.Log("Game Saved");
    }



    public void LoadGame()
    {
        Time.timeScale = 1;
        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.buildIndex);




    }



    public void NewGame()
    {
        PlayerPrefs.SetInt("SavedGame", 0);
        PlayerPrefs.SetInt("Flashlight", 0);
        HasFlashlight = false;
        PlayerPrefs.SetInt("Keycard", 0);
        HasKeycard = false;
        SaveSystem.DeleteFile();
        HasSavedFile = false;

    }

    #endregion




}
