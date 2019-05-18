//Author: Marcus Mellström

public abstract class EnemyAggroState : EnemyBaseState
{
    public override void Enter ()
    {
        //Debug.Log("Aggro State");
        Owner.LightSource.enabled = true;
        Owner.Agent.enabled = true;
        Owner.Agent.speed = Owner.MovementSpeed;
        Owner.Agent.acceleration = Owner.Acceleration;
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();
    }
}
