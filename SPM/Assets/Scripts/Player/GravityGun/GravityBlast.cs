//Author: Marcus Mellström

using System.Collections;
using UnityEngine;

public class GravityBlast : MonoBehaviour
{
    [SerializeField] private GameObject blastRadius;
    [SerializeField] private Light readyLight;
    [SerializeField] private new ParticleSystem particleSystem;
    [SerializeField] private float cooldownTime = 10.0f;
    [SerializeField] private float blastForce = 2000f;

    private float cooldownTimer = 0.0f;
    private bool isOnCooldown = false;

    private StateMachine gravityGun;

    public float BlastForce { get => blastForce; private set => blastForce = value; }

    private void Awake ()
    {
        gravityGun = GetComponent<GravityGun>();
    }

    private void Update ()
    {
        if (isOnCooldown == true) {
            cooldownTimer += Time.deltaTime;
            if(cooldownTimer >= cooldownTime) {
                isOnCooldown = false;
                cooldownTimer = 0.0f;
                readyLight.enabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && gravityGun.GetCurrentState() is GravityGunNotHoldingState && isOnCooldown == false) {
            particleSystem.Play();
            readyLight.enabled = false;
            isOnCooldown = true;
            Blast();
        }
    }

    public void Blast ()
    {
        blastRadius.SetActive(true);
    }
}
