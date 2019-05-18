using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    [SerializeField] private Light roomLight;
    [SerializeField] private Light buttonLight;

    private void Start()
    {
        roomLight.enabled = false;
        buttonLight.enabled = true;
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
