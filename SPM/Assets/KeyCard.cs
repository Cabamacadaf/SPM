using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Key");
            GameController.gameControllerInstance.hasKeycard = true;
            Destroy(this.gameObject);
        }
    }
}
