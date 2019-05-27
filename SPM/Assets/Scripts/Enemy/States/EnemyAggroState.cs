//Author: Marcus Mellström

using UnityEngine;

public abstract class EnemyAggroState : EnemyBaseState
{
    protected Vector3 RaycastDirection { get; set; }
    public override void Enter ()
    {
        //Debug.Log("Aggro State");
        Owner.LightSource.enabled = true;
        Owner.Obstacle.enabled = false;
        Owner.Agent.enabled = true;
        Owner.Agent.speed = Owner.AggroMovementSpeed;
        Owner.Agent.acceleration = Owner.Acceleration;
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        if (Owner.Agent.isOnNavMesh) {
            Owner.Agent.SetDestination(Owner.Player.transform.position);
        }
        RaycastDirection = Owner.transform.position - Owner.Player.Collider.bounds.center;
        base.HandleUpdate();
    }
}
