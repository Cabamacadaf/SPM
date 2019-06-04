using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    private void Awake ()
    {
        if (GameManager.instance.GameWasLoaded == true) {
            GameManager.instance.LoadGame();
        }
    }
}
