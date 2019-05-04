using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/BlastedState")]
public class EnemyBlastedState : EnemyBaseState
{
    private float timer;
    public override void Enter ()
    {
        timer = 0.0f;
        owner.agent.enabled = false;
        owner.rigidBody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        timer += Time.deltaTime;
        if(timer >= owner.blastRecoveryTime) {
            if(owner is Enemy1) {
                owner.Transition<Enemy1AggroState>();
            }
            if (owner is Enemy2) {
                owner.Transition<Enemy2AggroState>();
            }
        }
        base.HandleUpdate();
    }
    public override void Exit ()
    {
        owner.rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        base.Exit();
    }
}
