using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    [SerializeField] private GameObject pauseMenu;

    private void Awake()
    {
        instance = this;
        GameManager.instance.SetLoadingScreen(transform.Find("Loading Screen").gameObject);
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        GameManager.instance.GameIsPaused = true;
        CameraController.instance.UnLockCursor();
        pauseMenu.SetActive(true);

        Time.timeScale = 0;
    }
    public void Continue()
    {
        GameManager.instance.GameIsPaused = false;
        Time.timeScale = 1;
        CameraController.instance.LockCursor();
        pauseMenu.SetActive(false);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        GameManager.instance.RespawnPlayer();

        Continue();
    }

    public void SaveGame ()
    {
        GameManager.instance.SaveGame();
    }
}
