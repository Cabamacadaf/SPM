using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private void Awake()
    {
        pauseMenu.SetActive(false);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
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
}
