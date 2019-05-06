using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/BlastedState")]
public class EnemyBlastedState : EnemyBaseState
{
    private float timer;
    public override void Enter ()
    {
        //Debug.Log("Blasted State");
        timer = 0.0f;
        owner.agent.enabled = false;
        owner.rigidBody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        timer += Time.deltaTime;
        owner.agent.enabled = true;
        if (timer >= owner.blastRecoveryTime && owner.agent.isOnNavMesh) {
            if(owner is Enemy1) {
                owner.Transition<Enemy1AggroState>();
            }
            if (owner is Enemy2) {
                owner.Transition<Enemy2AggroState>();
            }
        }
        else {
            owner.agent.enabled = false;
        }
        base.HandleUpdate();
    }
    public override void Exit ()
    {
        owner.rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        base.Exit();
    }
}
