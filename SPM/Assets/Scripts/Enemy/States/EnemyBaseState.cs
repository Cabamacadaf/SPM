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

        Debug.Log(damage);
        owner.hitPoints -= damage;
        if (owner.hitPoints <= 0) {
            Kill();
        }
        owner.meshRenderer.material.color = owner.meshRenderer.material.color * owner.hitPoints / 100;
    }

    public void Kill ()
    {
        EnemyDeathEvent enemyDeathEvent = new EnemyDeathEvent(owner.gameObject);
        enemyDeathEvent.eventDescription = "Enemy " + owner.gameObject.name + " has died.";
        enemyDeathEvent.ExecuteEvent();
    }
}
