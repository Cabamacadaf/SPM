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
        if(GameManager.instance.HasSavedFile == false)
        {
            loadButton.interactable = false;
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


    public void LoadGame()
    {
        SceneManager.LoadScene(GameManager.instance.CurrentSceneIndex);
    }

    public void NewGame()
    {
        //GameManager.instance.NewGame();
        SceneManager.LoadScene(1);
    }

    public void ExitGame ()
    {
        Application.Quit();
    }
}
