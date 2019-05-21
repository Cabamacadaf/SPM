﻿//Author: Marcus Mellström

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
        if (Owner.GetCurrentState() is EnemyIdleState) {
            Owner.GetComponentInChildren<EnemyAggro>().Aggro();
        }
        
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
}
