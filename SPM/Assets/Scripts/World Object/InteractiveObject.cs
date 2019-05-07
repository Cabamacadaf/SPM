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

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();


            PlayerWalkState PC = player.GetCurrentState() as PlayerWalkState;
            if (PC != null)
            {
                player.Transition<PlayerCrouchState>();

            }
            else
            {

                player.Transition<PlayerWalkState>();
            }

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

