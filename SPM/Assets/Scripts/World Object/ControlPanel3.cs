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
        eButton.enabled = true;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GameController_2.gameControllerInstance_2.hasKeycard)
            {
                Vector3 tmp = button.transform.position;
                tmp.z = (tmp.z + 1);
                button.transform.position = tmp;
                button.GetComponent<Light>().color = Color.green;

                tmp = door.transform.position;
                tmp.z = (tmp.z - 8);
                door.transform.position = tmp;
                eButton.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
