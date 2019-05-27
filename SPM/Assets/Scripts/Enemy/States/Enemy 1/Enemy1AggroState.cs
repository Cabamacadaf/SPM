//Author: Marcus Mellström

using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/Enemy1/AggroState")]
public class Enemy1AggroState : EnemyAggroState
{
    public override void Enter ()
    {
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();
        if (!Physics.Raycast(Owner.Player.Collider.bounds.center, RaycastDirection.normalized, RaycastDirection.magnitude, Owner.WallLayer)) {
            if (Vector3.Distance(Owner.Player.transform.position, Owner.transform.position) <= Owner.AttackDistance) {
                Owner.Agent.enabled = false;
                Owner.Transition<EnemyAttackState>();
            }
        }
    }
}
