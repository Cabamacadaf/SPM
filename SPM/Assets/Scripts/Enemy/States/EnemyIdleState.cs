//Author: Marcus Mellström

using System.Collections;
using UnityEngine;

public abstract class EnemyIdleState : EnemyBaseState
{
    public override void Enter ()
    {
        //Debug.Log("Idle State");
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
        //Patrol();
        base.HandleUpdate();
    }

    //private void Patrol ()
    //{
    //    Debug.Log(Owner.PatrolArea.bounds.size);
    //    Vector3 randomPosition = new Vector3(Random.Range(0, Owner.PatrolArea.bounds.size.x), Random.Range(0, Owner.PatrolArea.bounds.size.y), Random.Range(0, Owner.PatrolArea.bounds.size.z));

    //}

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
