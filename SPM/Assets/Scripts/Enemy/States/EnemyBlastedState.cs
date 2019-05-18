//Author: Marcus Mellström

using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/BlastedState")]
public class EnemyBlastedState : EnemyBaseState
{
    private float timer;
    public override void Enter ()
    {
        //Debug.Log("Blasted State");
        timer = 0.0f;
        owner.Agent.enabled = false;
        owner.RigidBody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        timer += Time.deltaTime;
        owner.Agent.enabled = true;
        if (timer >= owner.BlastRecoveryTime && owner.Agent.isOnNavMesh) {
            if(owner is Enemy1) {
                owner.Transition<Enemy1AggroState>();
            }
            if (owner is Enemy2) {
                owner.Transition<Enemy2AggroState>();
            }
        }
        else {
            owner.Agent.enabled = false;
        }
        base.HandleUpdate();
    }
    public override void Exit ()
    {
        owner.RigidBody.constraints = RigidbodyConstraints.FreezeAll;
        base.Exit();
    }
}
