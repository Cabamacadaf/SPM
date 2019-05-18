//Author: Marcus Mellström

using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/BlastedState")]
public class EnemyBlastedState : EnemyBaseState
{
    private float timer;

    public override void Enter ()
    {
        Debug.Log("Blasted State");
        timer = 0.0f;
        Owner.Agent.enabled = false;
        Owner.RigidBody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        timer += Time.deltaTime;
        Owner.Agent.enabled = true;
        if (timer >= Owner.BlastRecoveryTime && Owner.Agent.isOnNavMesh) {
            if(Owner is Enemy1) {
                Owner.Transition<Enemy1AggroState>();
            }
            if (Owner is Enemy2) {
                Owner.Transition<Enemy2AggroState>();
            }
        }
        else {
            Owner.Agent.enabled = false;
        }
        base.HandleUpdate();
    }
    public override void Exit ()
    {
        Owner.RigidBody.constraints = RigidbodyConstraints.FreezeAll;
        base.Exit();
    }
}
