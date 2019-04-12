using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/AttackState")]
public class EnemyAttackState : EnemyBaseState
{
    private float timer = 0.0f;
    private Transform attackMesh;

    public override void Enter ()
    {
        base.Enter();
        timer = 0.0f;
        attackMesh = owner.transform.GetChild(0).GetChild(0);
        Attack();
    }

    private void Attack ()
    {

    }

    public override void HandleUpdate ()
    {
        if(timer >= owner.attackTime) {
            attackMesh.position = Vector3.zero;
            owner.Transition<EnemyAggroState>();
        }

        attackMesh.position += attackMesh.forward * Time.deltaTime * owner.attackAnimationSpeed;
        timer += Time.deltaTime;
    }
}
