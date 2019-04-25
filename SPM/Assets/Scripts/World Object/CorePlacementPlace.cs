using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorePlacementPlace : MonoBehaviour
{
    public GameObject objectA;
    private GameObject objectB;

    //Color color0 = Color.blue;
    Color color1 = new Color(0f, 145f, 0f, 5f);

    public Light lt;


    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("PowerCore"))
        {
            
            objectB = other.transform.gameObject;
            objectB.transform.position = objectA.transform.position;
            objectB.transform.parent = objectA.transform;
            GameController.gameControllerInstance.powerCoreCollection++;
            GameObject.Find("Objective_Power_Core").GetComponent<PickUpObject>().enabled = false;
            //stäng av Rigided body
            //byt ljusen till grönt!
            lt.color = (color1 / 2.0f) * Time.deltaTime;
        }
    }
}
