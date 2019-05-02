using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyListener : MonoBehaviour
{
    void Start()
    {
        EnemyDeathEvent.RegisterListener(OnEnemyDeath);
        KeycardPickedUpEvent.RegisterListener(OnKeyCardPickedUp);
    }

    void OnKeyCardPickedUp (KeycardPickedUpEvent keycardPickedUpEvent)
    {
        Destroy(keycardPickedUpEvent.gameObject);
    }

    void OnEnemyDeath (EnemyDeathEvent enemyDeathEvent)
    {
        Destroy(enemyDeathEvent.gameObject);
    }
}
