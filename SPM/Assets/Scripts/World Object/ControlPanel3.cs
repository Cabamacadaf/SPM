using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel3 : MonoBehaviour
{
    public GameObject door;
    public Text eButton;

    private GameObject button;
    
    // Start is called before the first frame update
    void Start()
    {
        eButton.enabled = false;
        button = this.gameObject;
        button.GetComponentInChildren<Light>().color = Color.red;
    }

    private void OnTriggerStay(Collider other)
    {
        eButton.text = "Press E";
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GameController_2.gameControllerInstance_2.hasKeycard)
            {
                Vector3 tmp = button.transform.position;
                tmp.z = (tmp.z + 0.5f);
                button.transform.position = tmp;
                button.GetComponentInChildren<Light>().color = Color.green;
                eButton.text = "";
                eButton.enabled = false;
                tmp = door.transform.position;
                //tmp.z = (tmp.z - 8);
                tmp.z = 246;
                door.transform.position = tmp;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        eButton.text = "";
    }
}
