//Author: Marcus Mellström

using System.Collections;
using UnityEngine;

public class GravityBlast : MonoBehaviour
{
    [SerializeField] private Collider blastRadius;
    [SerializeField] private Light readyLight;
    [SerializeField] private new ParticleSystem particleSystem;
    [SerializeField] private float cooldownTime = 10.0f;
    [SerializeField] private float blastForce = 2000f;

    private float cooldownTimer = 0.0f;
    private bool cooldown = false;

    private StateMachine gravityGun;

    public float BlastForce { get => blastForce; private set => blastForce = value; }

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
        blastRadius.enabled = true;
        yield return 0;
        blastRadius.enabled = false;
    }
}
