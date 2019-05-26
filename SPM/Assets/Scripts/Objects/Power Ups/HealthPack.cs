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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsInteractive )
        {
            playerHealth = PlayerTransform.GetComponent<HealthComponent>();
            playerHealth.Addhealth(healthToAdd);
            InteractText.text = "";
            InteractText.enabled = false;
            ObjectDestroyedEvent objectDestroyedEvent = new ObjectDestroyedEvent(gameObject);
            objectDestroyedEvent.ExecuteEvent();
        }
    }
}