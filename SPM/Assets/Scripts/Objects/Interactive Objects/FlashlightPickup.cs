//Author: Marcus Mellström

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightPickup : InteractiveObject
{
    [SerializeField] private float timeToShowMessage = 5.0f;

    private Text messageText;

    protected override void Awake ()
    {
        messageText = GameManager.CanvasInstance.transform.Find("Tutorial Text").GetComponent<Text>();
        base.Awake();
    }

    private void Update ()
    {
        if(Input.GetKeyDown(KeyCode.E) && IsInteractive) {
            PickUpFlashlight();
        }
    }

    private void PickUpFlashlight ()
    {
        GameManager.PlayerInstance.HasFlashlight = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponentInChildren<BoxCollider>().enabled = false;
        GetComponentInChildren<Light>().enabled = false;
        InteractText.enabled = false;
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
