using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel2 : MonoBehaviour
{
    public GameObject door;

    private void OnTriggerStay(Collider other)
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GameController_2.gameControllerInstance_2.hasKeycard)
            {
                door.SetActive(false);
                Debug.Log("I now Open the door for you");
            }
        }

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    if (door == isActiveAndEnabled)
        //    {
        //        door.SetActive(false);
        //        return;
        //    }

        //    else if (door.activeInHierarchy == false)
        //    {
        //        door.SetActive(true);
        //        return;
        //    }

        //}
    }
}
