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
    public int greenCollecting;

    void Start()
    {
        gameControllerInstance_2 = this;
        greenCollecting = 0;
        hasKeycard = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (greenCollecting == 16)
        {
            Debug.Log("You have reached your goal");
            OpenDoor();
            greenCollecting = 0;

        }
    }

    void OpenDoor()
    {
        door_Goal.SetActive(false);
        Debug.Log("The path is open");
        return;
    }
}
