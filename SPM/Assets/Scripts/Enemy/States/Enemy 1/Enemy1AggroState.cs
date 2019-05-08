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
        if (owner.agent.isOnNavMesh) {
            owner.agent.SetDestination(owner.player.transform.position);
        }

        if (Vector3.Distance(owner.player.transform.position, owner.transform.position) <= owner.attackDistance) {
            owner.agent.enabled = false;
            owner.Transition<EnemyAttackState>();
        }
        base.HandleUpdate();
    }
}
