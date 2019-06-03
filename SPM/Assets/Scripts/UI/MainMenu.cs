using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private Button loadButton;

    private void Awake ()
    {

        if(PlayerPrefs.GetInt("SavedGame") == 0)
        {
            loadButton.interactable = false;
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


    public void LoadGame()
    {
        Debug.Log("LoadGame Main menu");
        SceneManager.LoadScene(1);
        Time.timeScale = 1;

    }

    public void NewGame()
    {
        GameManager.instance.NewGame();
        SceneManager.LoadScene(1);
        Time.timeScale = 1;

    }

    public void ExitGame ()
    {
        Application.Quit();
    }
}
