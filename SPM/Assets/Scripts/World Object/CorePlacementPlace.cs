using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorePlacementPlace : MonoBehaviour
{
    public GameObject objectA;
    private GameObject objectB;

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("PowerCore"))
        {
            
            objectB = other.transform.gameObject;
            objectB.transform.position = objectA.transform.position;
            objectB.transform.parent = objectA.transform;
            GameController.gameControllerInstance.powerCoreCollection++;
            GameObject.Find("Objective_Power_Core").GetComponent<PickUpObject>().enabled = false;
        }
    }
}
