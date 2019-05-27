﻿//Author: Marcus Mellström

using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected Enemy Owner { get; set; }

    private Vector3 lastFramePosition;

    public override void Enter ()
    {
        if (Owner.GetCurrentState() is EnemyAggroState == false) {
            Owner.Obstacle.enabled = true;
        }
        base.Enter();
    }

    public override void Initialize (StateMachine owner)
    {
        Owner = (Enemy)owner;
        lastFramePosition = Owner.transform.position;
    }

    public override void HandleUpdate ()
    {
        Owner.CurrentVelocity = Mathf.Lerp(Owner.CurrentVelocity, (Owner.transform.position - lastFramePosition).magnitude / Time.deltaTime, Owner.VelocityInterpolation);
        lastFramePosition = Owner.transform.position;
        Owner.Animator.SetFloat("EnemySpeed", Owner.CurrentVelocity / Owner.AggroMovementSpeed);
        base.HandleUpdate();
    }

    public void Damage (float damage)
    {
        Owner.Transition<EnemyKnockbackState>();
        EnemyKnockbackState knockbackState = (EnemyKnockbackState)Owner.GetCurrentState();
        knockbackState.KnockBack(Owner.KnockbackForce, Owner.KnockbackRecoveryTime);

        Owner.HitPoints -= damage;

        if (Owner.HitPoints <= 0) {
            Kill();
        }
        Owner.MeshRenderer.material.color = Owner.Color * Owner.HitPoints / Owner.MaxHitPoints;
    }

    public void Kill ()
    {
        EnemyDeathEvent enemyDeathEvent = new EnemyDeathEvent(Owner.gameObject);
        enemyDeathEvent.EventDescription = "Enemy " + Owner.gameObject.name + " has died.";
        enemyDeathEvent.ExecuteEvent();
    }

    public void PlaySpawnSound ()
    {
        Owner.AudioSource.PlayOneShot(Owner.SpawnSound);
    }

    public void Aggro ()
    {
        if (Owner is Enemy1) {
            Owner.Transition<Enemy1AggroState>();
        }

        if (Owner is Enemy2) {
            Owner.Transition<Enemy2AggroState>();
        }
        EnemyAggroEvent enemyAggroEvent = new EnemyAggroEvent(Owner.AggroSound, Owner.AudioSource);
        enemyAggroEvent.ExecuteEvent();
    }
}
