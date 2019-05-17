//Author: Marcus Mellström

using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/Enemy1/AggroState")]
public class Enemy1AggroState : EnemyAggroState
{
    public override void Enter ()
    {
        base.Enter();
		owner.animator.SetFloat("Enemy1Speed", 1.0f);
    }

    public override void HandleUpdate ()
    {
        if (owner.agent.isOnNavMesh) {
            owner.agent.SetDestination(owner.player.transform.position);
        }
        Vector3 raycastDirection = owner.transform.position - owner.player.capsuleCollider.bounds.center;
        if (!Physics.Raycast(owner.player.capsuleCollider.bounds.center, raycastDirection.normalized, raycastDirection.magnitude, owner.wallLayer)) {
            if (Vector3.Distance(owner.player.transform.position, owner.transform.position) <= owner.attackDistance) {
                owner.agent.enabled = false;
                owner.Transition<EnemyAttackState>();
            }
        }
        base.HandleUpdate();
    }
}
