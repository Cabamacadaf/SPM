using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemStartTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particleSystems;

    private bool isTriggered;

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player") && isTriggered == false) {
            ActivateParticleSystems();
            isTriggered = true;
        }
    }

    private void ActivateParticleSystems ()
    {
        foreach (ParticleSystem particleSystem in particleSystems) {
            particleSystem.Play();
        }
    }
}
