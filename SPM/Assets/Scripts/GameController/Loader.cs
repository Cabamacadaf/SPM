using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject manager;
    // Start is called before the first frame update
    void Awake()
    {
        if(GameManager.instance == null)
        {
            Instantiate(manager);
        }
    }


}
