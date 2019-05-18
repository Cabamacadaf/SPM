//Author: Marcus Mellström

using System.Collections;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
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
        timeUntilSound = Random.Range(this.owner.IdleSoundMinTime, this.owner.IdleSoundMaxTime);
    }

    public override void HandleUpdate ()
    {
        timer += Time.deltaTime;
        if (timer >= timeUntilSound && !playing) {
            owner.AudioSource.PlayOneShot(owner.IdleSound);
            playing = true;
            timer = 0.0f;
        }

        if (playing && timer > owner.IdleSound.length) {
            playing = false;
            timer = 0.0f;
            timeUntilSound = Random.Range(owner.IdleSoundMinTime, owner.IdleSoundMaxTime);
        }

        base.HandleUpdate();
    }

    public override void Exit ()
    {
        owner.AudioSource.Stop();
        base.Exit();
    }
}
