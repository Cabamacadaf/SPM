//Author: Marcus Mellström

using UnityEngine;

public class HealthPack : InteractiveObject
{
    [SerializeField] private int id;
    [SerializeField] private float healthToAdd = 25.0f;
    private HealthComponent playerHealth;

    public int ID { get => id; set => id = value; }

    private new void Awake ()
    {
        LevelManager.Instance.AllHealthPacks.Add(ID, this);
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

            Destroy();
        }
    }

    public void Destroy()
    {
        ObjectDestroyedEvent objectDestroyedEvent = new ObjectDestroyedEvent(gameObject);
        objectDestroyedEvent.ExecuteEvent();
    }
}