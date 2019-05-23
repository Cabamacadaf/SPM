//Author: Marcus Mellström

using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/Enemy1/AggroState")]
public class Enemy1AggroState : EnemyAggroState
{
    public override void Enter ()
    {
        base.Enter();
        Owner.Animator.SetFloat("Enemy1Speed", 1.0f);
    }

    public override void HandleUpdate ()
    {
        Owner.Agent.SetDestination(Owner.Player.transform.position);
        Vector3 raycastDirection = Owner.transform.position - Owner.Player.Collider.bounds.center;
        if (!Physics.Raycast(Owner.Player.Collider.bounds.center, raycastDirection.normalized, raycastDirection.magnitude, Owner.WallLayer)) {
            if (Vector3.Distance(Owner.Player.transform.position, Owner.transform.position) <= Owner.AttackDistance) {
                Owner.Agent.enabled = false;
                Owner.Transition<EnemyAttackState>();
            }
        }
        base.HandleUpdate();
    }
}
