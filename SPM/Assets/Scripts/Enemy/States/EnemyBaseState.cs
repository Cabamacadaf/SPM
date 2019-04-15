using System.Collections;
using System.Collections.Generic;
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

    public override void HandleUpdate ()
    {
        if (owner.hitPoints < 0) {
            Kill();
        }
    }

    public void Damage (float speed, float mass, float damage)
    {
        Damage((speed * mass * damage) / 10);
    }

    public void Damage (float damage)
    {
        Debug.Log(damage);
        owner.hitPoints -= damage;
        owner.meshRenderer.material.color = owner.meshRenderer.material.color * owner.hitPoints / 100;
    }

    public void Kill ()
    {
        Destroy(owner.gameObject);
    }
}
