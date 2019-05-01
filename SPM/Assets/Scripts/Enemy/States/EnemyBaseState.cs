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
        if (owner.hitPoints <= 0) {
            Kill();
        }
    }

    public void Damage (float damage)
    {
        if (owner is Enemy2 && owner.GetCurrentState() is Enemy2IdleState) {
            owner.audioSource.PlayOneShot(owner.aggroSound);
            owner.Transition<Enemy2AggroState>();
        }
        else if (owner is Enemy1 && owner.GetCurrentState() is Enemy1IdleState) {
            owner.audioSource.PlayOneShot(owner.aggroSound);
            owner.Transition<Enemy1AggroState>();
        }
        Debug.Log(damage);
        owner.hitPoints -= damage;
        owner.meshRenderer.material.color = owner.meshRenderer.material.color * owner.hitPoints / 100;
    }

    public void Kill ()
    {
        Destroy(owner.gameObject);
    }
}
