//Author: Marcus Mellström

using UnityEngine;

public class HealthPack : InteractiveObject
{
    [SerializeField] private float healthToAdd = 25.0f;
    private Player player;
    private new void Awake ()
    {
        player = FindObjectOfType<Player>();
        base.Awake();
    }
    private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactive){
            player.Addhealth(healthToAdd);
            ObjectDestroyedEvent objectDestroyedEvent = new ObjectDestroyedEvent(gameObject);
            objectDestroyedEvent.ExecuteEvent();
        }
    }
}
