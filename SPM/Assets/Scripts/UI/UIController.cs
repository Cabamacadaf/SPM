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
        pauseMenu.SetActive(false);
    }

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

    public void LoadGame()
    {
        GameManager.instance.LoadGame();
    }
}
