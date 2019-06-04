//Author: Marcus Mellström

using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/BlastedState")]
public class EnemyKnockbackState : EnemyBaseState
{
    private float timer;

    private float recoveryTime;

    public override void Enter ()
    {
        //Debug.Log("Knockback State");
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        timer += Time.deltaTime;
        if (timer >= recoveryTime) {
            Owner.Obstacle.enabled = false;
            Owner.Agent.enabled = true;
            if (Owner.Agent.isOnNavMesh) {
                if (Owner is Enemy1) {
                    Owner.Transition<Enemy1AggroState>();
                }
                if (Owner is Enemy2) {
                    Owner.Transition<Enemy2AggroState>();
                }
            }
            else {
                Owner.Obstacle.enabled = true;
                Owner.Agent.enabled = false;
            }
        }
        base.HandleUpdate();
    }

    public void KnockBack (float knockbackForce, float recoveryTime)
    {
        this.recoveryTime = recoveryTime;
        timer = 0.0f;
        Owner.Agent.enabled = false;
        Owner.RigidBody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        Owner.RigidBody.AddForce((Owner.Collider.bounds.center - Owner.Player.transform.position).normalized * knockbackForce);
    }

    public override void Exit ()
    {
        Owner.RigidBody.constraints = RigidbodyConstraints.FreezeAll;
        base.Exit();
    }
}
