using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemStartTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particleSystems;

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player")) {
            ActivateParticleSystems();
        }
    }

    private void ActivateParticleSystems ()
    {
        foreach (ParticleSystem particleSystem in particleSystems) {
            particleSystem.Play();
        }
    }
}
