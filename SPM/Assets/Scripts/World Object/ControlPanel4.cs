using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel4 : MonoBehaviour
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
        if (other.CompareTag("Player"))
        {
            if (button.transform.position.x < 185)
            {
                eButton.enabled = true;
                eButton.text = "Press E";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (GameController_2.gameControllerInstance_2.hasKeycard)
                    {
                        Vector3 tmp = button.transform.position;
                        tmp.x = 185.2f;
                        button.transform.position = tmp;

                        eButton.text = "";
                        eButton.enabled = false;
                        Quaternion tmpp = button.transform.rotation;
                        tmpp.y = 0;
                        door.transform.rotation = tmpp;
                        button.GetComponentInChildren<Light>().color = Color.green;

                        button.GetComponent<ControlPanel3>().enabled = false;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        eButton.text = "";
    }
}
