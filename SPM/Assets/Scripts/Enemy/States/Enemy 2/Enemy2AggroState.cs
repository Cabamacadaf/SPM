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
        if (owner.agent.isOnNavMesh) {
            owner.agent.SetDestination(owner.player.transform.position);
        }

        if (Vector3.Distance(owner.player.transform.position, owner.transform.position) <= owner2.maxLeapRange && Vector3.Distance(owner.player.transform.position, owner.transform.position) >= owner2.minLeapRange) {
            owner.agent.enabled = false;
            owner.Transition<Enemy2LeapChargeState>();
        }

        if (Vector3.Distance(owner.player.transform.position, owner.transform.position) <= owner.attackDistance) {
            owner.agent.enabled = false;
            owner.Transition<EnemyAttackState>();
        }

        base.HandleUpdate();
    }
    public override void Exit ()
    {
        base.Exit();
    }
}
