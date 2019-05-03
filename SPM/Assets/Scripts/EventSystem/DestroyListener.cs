using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyListener : MonoBehaviour
{
    void Start()
    {
        EnemyDeathEvent.RegisterListener(OnEnemyDeath);
        KeycardPickedUpEvent.RegisterListener(OnKeyCardPickedUp);
        ObjectDestroyedEvent.RegisterListener(OnObjectDestroyed);
        ParticleSystemDestroyedEvent.RegisterListener(OnParticleSystemDestroyed);
    }

    void OnKeyCardPickedUp (KeycardPickedUpEvent keycardPickedUpEvent)
    {
        Destroy(keycardPickedUpEvent.gameObject);
    }

    void OnEnemyDeath (EnemyDeathEvent enemyDeathEvent)
    {
        Destroy(enemyDeathEvent.gameObject);
    }

    void OnObjectDestroyed(ObjectDestroyedEvent objectDestroyedEvent)
    {
        Destroy(objectDestroyedEvent.gameObject);
    }

    void OnParticleSystemDestroyed(ParticleSystemDestroyedEvent particleSystemDestroyedEvent)
    {
        Destroy(particleSystemDestroyedEvent.gameObject);
    }
}
