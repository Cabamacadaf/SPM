using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    private LoadScene loadScene;
    private void Awake ()
    {
        loadScene = GetComponent<LoadScene>();
        if (GameManager.instance.GameWasLoaded == true) {
            GameManager.instance.LoadGame();
        }
    }
}
