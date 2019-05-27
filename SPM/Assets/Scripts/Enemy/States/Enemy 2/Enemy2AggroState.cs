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
    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();
        if (!Physics.Raycast(Owner.Player.Collider.bounds.center, RaycastDirection.normalized, RaycastDirection.magnitude, Owner.WallLayer)) {
            if (Vector3.Distance(Owner.Player.transform.position, Owner.transform.position) <= owner2.MaxLeapRange
                    && Vector3.Distance(Owner.Player.transform.position, Owner.transform.position) >= owner2.MinLeapRange) {
                Owner.Agent.enabled = false;
                Owner.Transition<Enemy2LeapChargeState>();
            }

            if (Vector3.Distance(Owner.Player.transform.position, Owner.transform.position) <= Owner.AttackDistance) {
                Owner.Agent.enabled = false;
                Owner.Transition<EnemyAttackState>();
            }
        }
    }
}
