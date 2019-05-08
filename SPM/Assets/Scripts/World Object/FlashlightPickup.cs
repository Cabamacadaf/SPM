//Author: Marcus Mellström

using System.Collections;
using UnityEngine;

public class FlashlightPickup : InteractiveObject
{
    private void Update ()
    {
        if(Input.GetKeyDown(KeyCode.E) && interactive) {
            PickUpFlashlight();
        }
    }

    private void PickUpFlashlight ()
    {
        FindObjectOfType<Player>().hasFlashlight = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponentInChildren<BoxCollider>().enabled = false;
        GetComponentInChildren<Light>().enabled = false;
        StartCoroutine(DisplayHelpMessage());
    }

    private IEnumerator DisplayHelpMessage ()
    {
        yield return new WaitForSeconds(0.5f);
        interactText.text = "Press F to use Flashlight";
        yield return new WaitForSeconds(5.0f);
        interactText.text = "";
        interactText.enabled = false;
        Destroy(gameObject);
    }
}
