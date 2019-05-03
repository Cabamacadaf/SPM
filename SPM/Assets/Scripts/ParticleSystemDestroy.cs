using UnityEngine;

public class ParticleSystemDestroy : MonoBehaviour
{
    new private ParticleSystem particleSystem;

    private void Awake ()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (particleSystem != null && !particleSystem.IsAlive()) {
            ParticleSystemDestroyedEvent particleSystemDestroyedEvent = new ParticleSystemDestroyedEvent(gameObject);
            particleSystemDestroyedEvent.ExecuteEvent();
        }
    }
}
