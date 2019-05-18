//Author: Marcus Mellström

using UnityEngine;

public class HealthPack : InteractiveObject
{
    [SerializeField] private float healthToAdd = 25.0f;
    private HealthComponent playerHealth;
    private new void Awake ()
    {
        base.Awake();
    }


    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && IsInteractive && other.CompareTag("Player"))
        {
            playerHealth = other.GetComponent<HealthComponent>();
            playerHealth.Addhealth(healthToAdd);
            InteractText.text = "";
            InteractText.enabled = false;
            ObjectDestroyedEvent objectDestroyedEvent = new ObjectDestroyedEvent(gameObject);
            objectDestroyedEvent.ExecuteEvent();
        }
    }
}
