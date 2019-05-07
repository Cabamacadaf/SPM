using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveObject : MonoBehaviour
{
    private Text interactText;
    protected bool interactive = false;
    private void Awake ()
    {
        interactText = FindObjectOfType<Canvas>().GetComponentInChildren<Text>();
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player")) {
            interactText.enabled = true;
            interactive = true;
            interactText.text = "Press E to interact";
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Player")) {
            interactive = false;
            interactText.enabled = false;
            interactText.text = "";
        }
    }
}