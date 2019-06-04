using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    private GameObject loadingScreen;

    private float loadTime = 2.0f;
    private bool isActive = false;

    private float timer = 0.0f;

    public void Load (GameObject loadingScreen)
    {
        Time.timeScale = 0;
        this.loadingScreen = loadingScreen;
        this.loadingScreen.SetActive(true);
        isActive = true;
    }

    private void Update ()
    {
        if (isActive) {
            timer += Time.unscaledDeltaTime;
            if(timer >= loadTime) {
                loadingScreen.SetActive(false);
                isActive = false;
                timer = 0.0f;
                Time.timeScale = 1;
            }
        }
    }
}
