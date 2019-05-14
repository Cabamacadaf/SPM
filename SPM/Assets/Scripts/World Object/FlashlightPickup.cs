//Author: Marcus Mellström

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightPickup : InteractiveObject
{
    private Text messageText;
    [SerializeField] private float timeToShowMessage = 5.0f;

    private new void Awake ()
    {
        messageText = FindObjectOfType<Canvas>().transform.Find("Message Text").GetComponent<Text>();
        base.Awake();
    }

    private void Update ()
    {
        if(Input.GetKeyDown(KeyCode.E) && interactive) {
            PickUpFlashlight();
        }
    }

    private void PickUpFlashlight ()
    {
        FindObjectOfType<PlayerController>().hasFlashlight = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponentInChildren<BoxCollider>().enabled = false;
        GetComponentInChildren<Light>().enabled = false;
        interactText.enabled = false;
        StartCoroutine(DisplayHelpMessage());
    }

    private IEnumerator DisplayHelpMessage ()
    {
        messageText.enabled = true;
        messageText.text = "Press F to use Flashlight";
        yield return new WaitForSeconds(timeToShowMessage);
        messageText.text = "";
        messageText.enabled = false;
        Destroy(gameObject);
    }
}
