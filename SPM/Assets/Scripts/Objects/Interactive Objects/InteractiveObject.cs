//Author: Marcus Mellström

using UnityEngine;
using UnityEngine.UI;

public class InteractiveObject : MonoBehaviour
{
    protected string TextToSet { get; set; }
    protected Text InteractText { get; set; }
    protected bool IsInteractive { get; set; }

    protected void Awake ()
    {
        InteractText = FindObjectOfType<Canvas>().transform.Find("Interaction Text").GetComponent<Text>();
        TextToSet = "Press E to interact";
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

