//Author: Marcus Mellström

using UnityEngine;

public class ParticleSystemDestroy : MonoBehaviour
{
    private new ParticleSystem particleSystem;

    private void Awake ()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (particleSystem != null && !particleSystem.IsAlive()) {
            ParticleSystemDestroyedEvent particleSystemDestroyedEvent = new ParticleSystemDestroyedEvent(gameObject);
            particleSystemDestroyedEvent.ExecuteEvent();
        }
    }
}
