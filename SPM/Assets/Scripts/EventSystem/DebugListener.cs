//Author: Marcus Mellström

using UnityEngine;

public class DebugListener : MonoBehaviour
{
    private void Start ()
    {
        PowerCorePlacedEvent.RegisterListener(OnPowerCorePlaced);
        EnemyDeathEvent.RegisterListener(OnEnemyDeath);
        KeycardPickedUpEvent.RegisterListener(OnKeyCardPickedUp);
    }

    void OnPowerCorePlaced (PowerCorePlacedEvent powerCorePlacedEvent)
    {
        Debug.Log(powerCorePlacedEvent.eventDescription);
    }

    void OnEnemyDeath (EnemyDeathEvent enemyDeathEvent)
    {
        Debug.Log(enemyDeathEvent.eventDescription);
    }

    void OnKeyCardPickedUp (KeycardPickedUpEvent keycardPickedUpEvent)
    {
        Debug.Log(keycardPickedUpEvent.eventDescription);
    }
}
