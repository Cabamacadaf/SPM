using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel2 : MonoBehaviour
{
    public GameObject door;
    private GameObject button;
    public Light light1, light2, light3, light4, light5;
    public Text eButton;
    //public GameObject doorLight;

    private void Start()
    {
        light1.enabled = false;
        light2.enabled = false;
        light3.enabled = false;
        light4.enabled = false;
        light5.enabled = false;
        eButton.enabled = false;
        button = this.gameObject;
        button.GetComponentInChildren<Light>().color = Color.red;

        //doorLight.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            eButton.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (GameController_2.gameControllerInstance_2.hasKeycard)
                {
                    //door.SetActive(false);
                    light1.enabled = true;
                    light2.enabled = true;
                    light3.enabled = true;
                    light4.enabled = true;
                    light5.enabled = true;
                    Debug.Log("I now Open the door for you");
                    eButton.text = "";
                    if(button.transform.position.z > 82)
                    {
                        Vector3 tmpp = button.transform.position;
                        tmpp.z = (tmpp.z - 0.5f);
                        button.transform.position = tmpp;
                    }
                  
                    button.GetComponentInChildren<Light>().color = Color.green;

                    //Vector3 tmp = door.transform.position;
                    //tmp.z = 270;
                    //door.transform.position = tmp;
                    door.SetActive(false);
                    //doorLight.SetActive(true);
                }
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

    private void OnTriggerExit(Collider other)
    {

        eButton.enabled = false;
    }


}
