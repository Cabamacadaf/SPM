//Author: Marcus Mellström

using UnityEngine;

public abstract class EnemyBaseState : State
{

    protected Enemy Owner { get; set; }

    public override void Enter ()
    {
        if(Owner.GetCurrentState() is EnemyAggroState == false) {
            Owner.Obstacle.enabled = true;
        }
        base.Enter();
    }

    public override void Initialize (StateMachine owner)
    {
        Owner = (Enemy)owner;
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
