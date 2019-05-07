using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPuzzleTrigger : MonoBehaviour
{
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PuzzleObject"))
        {
            
            other.transform.position = transform.position;
            other.transform.parent = transform;
            other.transform.rotation = Quaternion.identity;
            other.gameObject.layer = 0;
            Destroy(other.GetComponent<Rigidbody>());
            
            GameController_2.gameControllerInstance_2.greenCollecting++;
            //StartCoroutine(ChangeColors());

            //objectB = other.transform.gameObject;
            //objectB.transform.position = objectA.transform.position;
            //objectB.transform.parent = objectA.transform;
            //GameController.gameControllerInstance.powerCoreCollection++;
            //GameObject.Find("Objective_Power_Core").GetComponent<PickUpObject>().enabled = false;
            ////stäng av Rigided body
            //objectB.gameObject.SetActive(false);
            ////byt ljusen till grönt!
            //lt.color = (color1 / 2.0f) * Time.deltaTime;
        }
    }
}
