using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    public GameObject door;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E)){
            if (GameController.gameControllerInstance.hasKeycard)
            {
                door.SetActive(false);
            }
        }
    }
}
