using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveObject : MonoBehaviour
{
    protected string textToSet = "Press E to interact";
    protected Text interactText;
    protected bool interactive = false;

    protected void Awake ()
    {
        interactText = FindObjectOfType<Canvas>().GetComponentInChildren<Text>();
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player")) {
            interactText.enabled = true;
            interactive = true;
            interactText.text = textToSet;
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