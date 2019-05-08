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
    public GameObject[] lights;

    [HideInInspector] //ni får skriva det här med eran andra version
    public int greenCollecting;

    void Start()
    {
        setAllLightFalse();
        gameControllerInstance_2 = this;
        greenCollecting = 0;
        hasKeycard = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(greenCollecting);
        if (greenCollecting == 9)
        {
            Debug.Log("You have reached your goal");
            OpenDoor();
            greenCollecting = 0;
            setAllLightTrue();
        }
    }

    void OpenDoor()
    {
        door_Goal.GetComponent<SplittingDoor>().Open();
        Debug.Log("The path is open");
        return;
    }

    private void setAllLightFalse ()
    {
        foreach(GameObject light in lights) {
            light.SetActive(false);
        }
    }

    private void setAllLightTrue ()
    {
        foreach (GameObject light in lights) {
            light.SetActive(true);
        }
    }
}
