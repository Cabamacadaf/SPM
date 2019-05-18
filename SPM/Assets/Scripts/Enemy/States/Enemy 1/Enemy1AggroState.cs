//Author: Marcus Mellström

using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/Enemy1/AggroState")]
public class Enemy1AggroState : EnemyAggroState
{
    public override void Enter ()
    {
        base.Enter();
        owner.Animator.SetFloat("Enemy1Speed", 1.0f);
    }

    public override void HandleUpdate ()
    {
        owner.Agent.SetDestination(owner.Player.transform.position);
        Vector3 raycastDirection = owner.transform.position - owner.Player.capsuleCollider.bounds.center;
        if (!Physics.Raycast(owner.Player.capsuleCollider.bounds.center, raycastDirection.normalized, raycastDirection.magnitude, owner.WallLayer)) {
            if (Vector3.Distance(owner.Player.transform.position, owner.transform.position) <= owner.AttackDistance) {
                owner.Agent.enabled = false;
                owner.Transition<EnemyAttackState>();
            }
        }
        base.HandleUpdate();
    }
}
