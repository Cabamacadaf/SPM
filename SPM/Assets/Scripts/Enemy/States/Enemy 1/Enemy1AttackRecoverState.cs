using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy1/AttackRecoverState")]
public class Enemy1AttackRecoverState : EnemyBaseState
{
    private float timer;
    private Enemy1 owner1;

    public override void Initialize (StateMachine owner)
    {
        base.Initialize(owner);
        owner1 = (Enemy1)owner;
    }

    public override void Enter ()
    {
        //Debug.Log("Attack Recover State");
        base.Enter();
        timer = 0.0f;
        owner.attackObject.SetActive(false);
    }

    public override void HandleUpdate ()
    {
        owner.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(owner.transform.forward, owner.player.transform.position - owner.transform.position, owner.rotationSpeed * Time.deltaTime, 0.0f));

        timer += Time.deltaTime;

        if(timer >= owner1.attackCooldown) {
            owner.Transition<Enemy1AggroState>();
        }
        base.HandleUpdate();
    }
}
