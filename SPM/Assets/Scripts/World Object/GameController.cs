using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController gameControllerInstance;
    public GameObject keyCard;
    public Transform keySpawnPoint;
    [HideInInspector]
    public bool hasKeycard;

    [HideInInspector] //ni får skriva det här med eran andra version
    public int powerCoreCollection;

    void Start()
    {
        gameControllerInstance = this;
        powerCoreCollection = 0;
        hasKeycard = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(hasKeycard);
       
        if (powerCoreCollection == 4)
        {
            //spawna keyCard
            KeySpawn();
            powerCoreCollection = 0;

        }
    }

    void KeySpawn ()
    {
        Instantiate(keyCard, keySpawnPoint.position, Quaternion.identity);
    }
}
