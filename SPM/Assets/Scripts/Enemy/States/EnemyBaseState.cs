//Author: Marcus Mellström

using UnityEngine;

public abstract class EnemyBaseState : State
{

    protected Enemy Owner { get; set; }

    public override void Enter ()
    {
        base.Enter();
    }

    public override void Initialize (StateMachine owner)
    {
        Owner = (Enemy)owner;
    }

    public void Damage (float damage)
    {
        if ((Owner is Enemy2 && Owner.GetCurrentState() is Enemy2IdleState) || (Owner is Enemy1 && Owner.GetCurrentState() is Enemy1IdleState)) {
            Owner.GetComponentInChildren<EnemyAggro>().Aggro();
        }
        
        Owner.HitPoints -= damage;
        if (Owner.HitPoints <= 0) {
            Kill();
        }
        Owner.MeshRenderer.material.color = Owner.MeshRenderer.material.color * Owner.HitPoints / Owner.MaxHitPoints;
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
