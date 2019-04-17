using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy2/LeapRecoverState")]
public class EnemyLeapRecoverState : EnemyBaseState
{
    private Enemy2 owner2;
    private float timer;

    public override void Enter ()
    {
        base.Enter();
        owner2 = (Enemy2)owner;
        owner2.attacking = false;
        owner2.mouthCollider.enabled = false;
        owner2.mouthRenderer.enabled = false;
    }
    public override void HandleUpdate ()
    {
        base.HandleUpdate();
        timer += Time.deltaTime;
        if (timer >= owner2.leapRecovery) {
            owner.Transition<Enemy2AggroState>();
        }
    }
}
