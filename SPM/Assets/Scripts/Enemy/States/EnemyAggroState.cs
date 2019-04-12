using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/AggroState")]
public class EnemyAggroState : EnemyBaseState
{
    [SerializeField] private float chaseDistance = 6.0f;
    [SerializeField] private float attackDistance = 2.0f;
    public override void Enter ()
    {
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();

        if(Vector3.Distance(owner.player.transform.position, owner.transform.position) > attackDistance) {
            owner.transform.position = Vector3.MoveTowards(owner.transform.position, owner.player.transform.position, movementSpeed * Time.deltaTime);

            owner.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(owner.transform.forward, owner.player.transform.position - owner.transform.position, rotationSpeed * Time.deltaTime, 0.0f));
        }

        if (Vector3.Distance(owner.player.transform.position, owner.transform.position) > chaseDistance) {
            owner.Transition<EnemyIdleState>();
        }
    }
}
