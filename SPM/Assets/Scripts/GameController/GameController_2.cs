using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class GameController_2 : MonoBehaviour
{
    public static GameController_2 gameControllerInstance_2;
    //public GameObject keyCard;
    //public Transform keySpawnPoint;
    [HideInInspector]
    public bool hasKeycard;
    public GameObject door_Goal;

    [HideInInspector] //ni får skriva det här med eran andra version
    public int powerCoreCollection;

    void Start()
    {
        gameControllerInstance_2 = this;
        powerCoreCollection = 0;
        hasKeycard = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (powerCoreCollection == 16)
        {
            //spawna keyCard
            OpenDoor();
            powerCoreCollection = 0;

        }
    }

    void OpenDoor()
    {
        door_Goal.SetActive(true);
        return;
    }
}
