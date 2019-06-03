//Author: Marcus Mellström

using UnityEngine;

public class DestroyListener : MonoBehaviour
{
    private void Start()
    {
        KeycardPickedUpEvent.RegisterListener(OnKeyCardPickedUp);
        ObjectDestroyedEvent.RegisterListener(OnObjectDestroyed);
        ParticleSystemDestroyedEvent.RegisterListener(OnParticleSystemDestroyed);
        DeathEvent.RegisterListener(OnPlayerDeath);
    }

    private void OnPlayerDeath (DeathEvent deathEvent)
    {
        GameManager.Instance.RespawnPlayer();
    }

    private void OnKeyCardPickedUp (KeycardPickedUpEvent keycardPickedUpEvent)
    {
        Destroy(keycardPickedUpEvent.gameObject);
    }

    private void OnObjectDestroyed (ObjectDestroyedEvent objectDestroyedEvent)
    {
        Destroy(objectDestroyedEvent.gameObject);
    }

    private void OnParticleSystemDestroyed (ParticleSystemDestroyedEvent particleSystemDestroyedEvent)
    {
        Destroy(particleSystemDestroyedEvent.gameObject);
    }
}
