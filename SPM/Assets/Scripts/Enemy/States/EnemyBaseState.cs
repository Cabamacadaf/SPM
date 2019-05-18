//Author: Marcus Mellström

using UnityEngine;

public class EnemyBaseState : State
{

    protected Enemy owner;

    public override void Enter ()
    {
        base.Enter();
    }

    public override void Initialize (StateMachine owner)
    {
        this.owner = (Enemy)owner;
    }

    public void Damage (float damage)
    {
        if ((owner is Enemy2 && owner.GetCurrentState() is Enemy2IdleState) || (owner is Enemy1 && owner.GetCurrentState() is Enemy1IdleState)) {
            owner.GetComponentInChildren<EnemyAggro>().Aggro();
        }
        
        owner.HitPoints -= damage;
        if (owner.HitPoints <= 0) {
            Kill();
        }
        owner.MeshRenderer.material.color = owner.MeshRenderer.material.color * owner.HitPoints / 100;
    }

    public void Kill ()
    {
        EnemyDeathEvent enemyDeathEvent = new EnemyDeathEvent(owner.gameObject);
        enemyDeathEvent.eventDescription = "Enemy " + owner.gameObject.name + " has died.";
        enemyDeathEvent.ExecuteEvent();
    }

    public void PlaySpawnSound ()
    {
        owner.AudioSource.PlayOneShot(owner.SpawnSound);
    }
}
