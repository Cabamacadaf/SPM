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

    private float timer = 0.0f;
    private float timeUntilSound;
    private bool playing = false;

    public override void Initialize (StateMachine owner)
    {
        base.Initialize(owner);
        timeUntilSound = Random.Range(this.Owner.IdleSoundMinTime, this.Owner.IdleSoundMaxTime);
    }

    public override void HandleUpdate ()
    {
        timer += Time.deltaTime;
        if (timer >= timeUntilSound && !playing) {
            Owner.AudioSource.PlayOneShot(Owner.IdleSound);
            playing = true;
            timer = 0.0f;
        }

        if (playing && timer > Owner.IdleSound.length) {
            playing = false;
            timer = 0.0f;
            timeUntilSound = Random.Range(Owner.IdleSoundMinTime, Owner.IdleSoundMaxTime);
        }

        base.HandleUpdate();
    }

    public override void Exit ()
    {
        Owner.AudioSource.Stop();
        base.Exit();
    }
}
