﻿//Author: Marcus Mellström

using UnityEngine;
using UnityEngine.UI;

public abstract class InteractiveObject : MonoBehaviour
{
    [SerializeField] private string textToSet = "Press E to interact";

    protected string TextToSet { get => textToSet; set => textToSet = value; }
    protected Text InteractText { get; set; }
    protected bool IsInteractive { get; set; }

    protected virtual void Awake ()
    {
        InteractText = FindObjectOfType<Canvas>().transform.Find("Interaction Text").GetComponent<Text>();
        IsInteractive = false;
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player")) {
            InteractText.enabled = true;
            IsInteractive = true;
            InteractText.text = TextToSet;
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Player")) {
            IsInteractive = false;
            InteractText.enabled = false;
            InteractText.text = "";
        }
    }
}

