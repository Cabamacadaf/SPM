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
            if (door == isActiveAndEnabled)
            {
                door.SetActive(false);
                return;
            }

            else if (door.activeInHierarchy == false)
            {
                door.SetActive(true);
                return;
            }

        }
    }
}
