//Author: Marcus Mellström

using System.Collections;
using UnityEngine;

public abstract class EnemyIdleState : EnemyBaseState
{
    private Vector3 randomPosition;
    private float minDistanceToDestination = 0.5f;
    private bool destinationReached = true;

    public override void Enter ()
    {
        //Debug.Log("Idle State");
        if (Owner.PatrolArea != null) {
            Owner.Agent.enabled = true;
            Owner.Agent.speed = Owner.IdleMovementSpeed;
            Owner.Agent.acceleration = Owner.Acceleration;
        }
        base.Enter();
    }

    private float soundTimer = 0.0f;
    private float timeUntilSound;
    private bool soundIsPlaying = false;

    public override void Initialize (StateMachine owner)
    {
        base.Initialize(owner);
        timeUntilSound = Random.Range(Owner.IdleSoundMinTime, Owner.IdleSoundMaxTime);
    }

    public override void HandleUpdate ()
    {
        HandleSound();
        if (Owner.PatrolArea != null) {
            Patrol();
        }
        base.HandleUpdate();
    }

    private void Patrol ()
    {
        if (destinationReached == true) {
            destinationReached = false;
            randomPosition = new Vector3(Random.Range(Owner.PatrolArea.bounds.min.x, Owner.PatrolArea.bounds.max.x),
                Random.Range(Owner.PatrolArea.bounds.min.y, Owner.PatrolArea.bounds.max.y),
                Random.Range(Owner.PatrolArea.bounds.min.z, Owner.PatrolArea.bounds.max.z));
            Owner.Agent.SetDestination(randomPosition);
        }

        if (Vector3.Distance(Owner.transform.position, randomPosition) <= minDistanceToDestination){
            Debug.Log("Destination reached");
            destinationReached = true;
        }
    }

    private void HandleSound ()
    {
        soundTimer += Time.deltaTime;
        if (soundTimer >= timeUntilSound && !soundIsPlaying) {
            Owner.AudioSource.PlayOneShot(Owner.IdleSound);
            soundIsPlaying = true;
            soundTimer = 0.0f;
        }

        if (soundIsPlaying && soundTimer > Owner.IdleSound.length) {
            soundIsPlaying = false;
            soundTimer = 0.0f;
            timeUntilSound = Random.Range(Owner.IdleSoundMinTime, Owner.IdleSoundMaxTime);
        }
    }

    public override void Exit ()
    {
        Owner.AudioSource.Stop();
        base.Exit();
    }
}
