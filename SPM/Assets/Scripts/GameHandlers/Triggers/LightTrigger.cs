using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{

    public Light roomLight;
    public Light buttonLight;
    // Start is called before the first frame update
    void Start()
    {
        roomLight.enabled = false;
        buttonLight.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player")) {
            roomLight.enabled = true;
            buttonLight.enabled = false;
        }
        if (other.CompareTag("Shootable")) {
            roomLight.enabled = true;
            buttonLight.enabled = false;
        }
    }
}
