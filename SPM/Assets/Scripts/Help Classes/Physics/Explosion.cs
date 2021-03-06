﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Header("ParticleEffects")]
    [SerializeField] private GameObject horizontalParticleEffect;
    [SerializeField] private GameObject verticalParticaleEffect;

    [Header("Explosion")]
    [SerializeField] private float power;
    [SerializeField] private float damage;
    [SerializeField] private float radius;
    [Tooltip("Adjustment to the apparent position of the explosion to make it seem to lift objects.")]
    [SerializeField] private float upwardsModifier;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shootable"))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider collider in colliders)
            {
                AddExplosionForce(collider);

                InstantiateParticleEffects();

                HandleSpecificObjects(collider);

                Destroy(this.gameObject);

            }
        }


    }

    private void HandleSpecificObjects(Collider collider)
    {
        Vector3 hit;
        if (collider.CompareTag("Player"))
        {
            hit = collider.ClosestPoint(transform.position);

            HealthComponent player = collider.gameObject.GetComponent<HealthComponent>();
            player.Damage(damage - hit.x - hit.z);
        }

        if (collider.CompareTag("Enemy"))
        {
            hit = collider.ClosestPoint(transform.position);

            EnemyBaseState enemyState = (EnemyBaseState)collider.GetComponent<Enemy>().GetCurrentState();

            //enemyState.Damage(damage - hit.x - hit.z);
            enemyState.Damage(damage);
        }

        if (collider.CompareTag("EnemyMouth"))
        {
            Enemy2 enemy = collider.GetComponentInParent<Enemy2>();
            EnemyBaseState enemyState = (EnemyBaseState)enemy.GetCurrentState();

            enemyState.Damage(damage * enemy.MouthDamageMultiplier);
        }
    }

    private void AddExplosionForce(Collider collider)
    {
        Vector3 explosionPosistion = transform.position;
        Rigidbody rb = collider.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddExplosionForce(power, explosionPosistion, radius, upwardsModifier);
        }
    }

    private void InstantiateParticleEffects()
    {
        Instantiate(horizontalParticleEffect, transform.position, Quaternion.identity);
        Instantiate(verticalParticaleEffect, transform.position, Quaternion.identity);
    }
}
