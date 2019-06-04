using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button loadButton;
    [SerializeField] private LoadScene loadScene;

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
        GameManager.instance.GameWasLoaded = true;
        loadScene.Load(PlayerPrefs.GetInt("CurrentLevel"));
    }

    public void NewGame()
    {
        GameManager.instance.NewGame();
        GameManager.instance.GameWasLoaded = false;
        loadScene.Load(1);
    }

    public void ExitGame ()
    {
        Application.Quit();
    }
}
