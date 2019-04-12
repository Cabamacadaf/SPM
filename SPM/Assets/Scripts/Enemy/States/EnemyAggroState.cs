using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/AggroState")]
public class EnemyAggroState : EnemyBaseState
{
    
    public override void Enter ()
    {
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();
        if(Vector3.Distance(owner.player.transform.position, owner.transform.position) > owner.attackDistance) {
            owner.transform.position = Vector3.MoveTowards(owner.transform.position, owner.player.transform.position, owner.movementSpeed * Time.deltaTime);

            owner.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(owner.transform.forward, owner.player.transform.position - owner.transform.position, owner.rotationSpeed * Time.deltaTime, 0.0f));
        }
        else {
            owner.Transition<EnemyAttackState>();
        }

        if (Vector3.Distance(owner.player.transform.position, owner.transform.position) > owner.chaseDistance) {
            owner.Transition<EnemyIdleState>();
        }
    }
}
