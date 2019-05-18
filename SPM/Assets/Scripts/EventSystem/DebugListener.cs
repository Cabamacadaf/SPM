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

    private void OnPowerCorePlaced (PowerCorePlacedEvent powerCorePlacedEvent)
    {
        Debug.Log(powerCorePlacedEvent.EventDescription);
    }

    private void OnEnemyDeath (EnemyDeathEvent enemyDeathEvent)
    {
        Debug.Log(enemyDeathEvent.EventDescription);
    }

    private void OnKeyCardPickedUp (KeycardPickedUpEvent keycardPickedUpEvent)
    {
        Debug.Log(keycardPickedUpEvent.EventDescription);
    }
}
