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
        timeUntilSound = Random.Range(this.owner.idleSoundMinTime, this.owner.idleSoundMaxTime);
    }

    public override void HandleUpdate ()
    {
        timer += Time.deltaTime;
        if (timer >= timeUntilSound && !playing) {
            owner.audioSource.PlayOneShot(owner.idleSound);
            playing = true;
            timer = 0.0f;
        }

        if (playing && timer > owner.idleSound.length) {
            playing = false;
            timer = 0.0f;
            timeUntilSound = Random.Range(owner.idleSoundMinTime, owner.idleSoundMaxTime);
        }

        base.HandleUpdate();
    }

    public override void Exit ()
    {
        owner.audioSource.Stop();
        base.Exit();
    }
}
