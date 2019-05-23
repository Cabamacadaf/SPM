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
		Owner.Animator.SetFloat("Enemy2Speed", 1.0f);
    }

    public override void HandleUpdate ()
    {
        if (Owner.Agent.isOnNavMesh) {
            Owner.Agent.SetDestination(Owner.Player.transform.position);
        }

        Vector3 raycastDirection = Owner.transform.position - Owner.Player.Collider.bounds.center;
        if (!Physics.Raycast(Owner.Player.Collider.bounds.center, raycastDirection.normalized, raycastDirection.magnitude, Owner.WallLayer)) {
            if (Vector3.Distance(Owner.Player.transform.position, Owner.transform.position) <= owner2.MaxLeapRange && Vector3.Distance(Owner.Player.transform.position, Owner.transform.position) >= owner2.MinLeapRange) {
                Owner.Agent.enabled = false;
                Owner.Transition<Enemy2LeapChargeState>();
            }

            if (Vector3.Distance(Owner.Player.transform.position, Owner.transform.position) <= Owner.AttackDistance) {
                Owner.Agent.enabled = false;
                Owner.Transition<EnemyAttackState>();
            }
        }
        base.HandleUpdate();
    }

    public override void Exit ()
    {
        base.Exit();
    }
}
