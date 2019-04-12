using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : State
{
    [SerializeField] protected float hitPoints = 100f;
    [SerializeField] protected float movementSpeed = 5.0f;

    protected Enemy owner;

    public override void Enter ()
    {
        owner.agent.speed = movementSpeed;
        base.Enter();

    }

    public override void Initialize (StateMachine owner)
    {
        this.owner = (Enemy)owner;
    }

    public override void HandleUpdate ()
    {
        if (hitPoints < 0) {
            Debug.Log("Kill enemy");
            Kill();
        }
    }

    public void Damage (float speed, float damage)
    {
        Debug.Log("Damage: " + (speed * damage) / 10);
        Damage((speed * damage / 10));
    }

    public void Damage (float damage)
    {
        hitPoints -= damage;
        owner.meshRenderer.material.color = owner.meshRenderer.material.color * hitPoints / 100;
    }

    public void Kill ()
    {
        Destroy(owner.gameObject);
    }
}
