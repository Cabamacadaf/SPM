//Author: Marcus Mellström

using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/Enemy2/AggroState")]
public class Enemy2AggroState : EnemyAggroState
{
    private Enemy2 owner2;

    public override void Initialize (StateMachine owner)
    {
        base.Initialize(owner);
        owner2 = (Enemy2)owner;
    }

    public override void Enter ()
    {
        base.Enter();
		owner.Animator.SetFloat("Enemy2Speed", 1.0f);
    }

    public override void HandleUpdate ()
    {
        if (owner.Agent.isOnNavMesh) {
            owner.Agent.SetDestination(owner.Player.transform.position);
        }

        Vector3 raycastDirection = owner.transform.position - owner.Player.capsuleCollider.bounds.center;
        if (!Physics.Raycast(owner.Player.capsuleCollider.bounds.center, raycastDirection.normalized, raycastDirection.magnitude, owner.WallLayer)) {
            if (Vector3.Distance(owner.Player.transform.position, owner.transform.position) <= owner2.MaxLeapRange && Vector3.Distance(owner.Player.transform.position, owner.transform.position) >= owner2.MinLeapRange) {
                owner.Agent.enabled = false;
                owner.Transition<Enemy2LeapChargeState>();
            }

            if (Vector3.Distance(owner.Player.transform.position, owner.transform.position) <= owner.AttackDistance) {
                owner.Agent.enabled = false;
                owner.Transition<EnemyAttackState>();
            }
        }
        base.HandleUpdate();
    }

    public override void Exit ()
    {
        base.Exit();
    }
}
