using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/AggroState")]
public class EnemyAggroState : EnemyBaseState
{
    private float timer;

    private void Awake ()
    {
        timer = owner.attackCooldown;
    }

    public override void Enter ()
    {
        base.Enter();
        timer = 0.0f;
    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();
        timer += Time.deltaTime;

        if (timer > owner.attackCooldown){
            owner.Transition<EnemyAttackState>();
        }

        if (Vector3.Distance(owner.player.transform.position, owner.transform.position) > owner.chaseDistance) {
            owner.Transition<EnemyIdleState>();
        }
    }
}
