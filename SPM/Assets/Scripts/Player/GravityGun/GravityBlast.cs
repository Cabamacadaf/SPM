//Author: Marcus Mellström

using System.Collections;
using UnityEngine;

public class GravityBlast : MonoBehaviour
{
    [SerializeField] private Collider blastRadius;
    [SerializeField] private Renderer meshRenderer;
    [SerializeField] private Light readyLight;
    [SerializeField] private new ParticleSystem particleSystem;
    [SerializeField] private float cooldownTime = 10.0f;
    private float cooldownTimer = 0.0f;
    private bool cooldown = false;
    private StateMachine gravityGun;

    public float blastForce = 50000.0f;

    private void Awake ()
    {
        gravityGun = GetComponent<GravityGun>();
    }

    private void Update ()
    {
        if (cooldown) {
            cooldownTimer += Time.deltaTime;
            if(cooldownTimer >= cooldownTime) {
                cooldown = false;
                cooldownTimer = 0.0f;
                readyLight.enabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && gravityGun.GetCurrentState() is GravityGunNotHoldingState && !cooldown) {
            particleSystem.Play();
            readyLight.enabled = false;
            cooldown = true;
            Blast();
        }
    }

    public void Blast ()
    {
        StartCoroutine(EnableTrigger());
    }

    IEnumerator EnableTrigger ()
    {
        meshRenderer.enabled = true;
        blastRadius.enabled = true;
        yield return 0;
        meshRenderer.enabled = false;
        blastRadius.enabled = false;
    }
}
