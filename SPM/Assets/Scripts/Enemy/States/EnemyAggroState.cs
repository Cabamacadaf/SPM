//Author: Marcus Mellström

public class EnemyAggroState : EnemyBaseState
{
    public override void Enter ()
    {
        //Debug.Log("Aggro State");
        owner.LightSource.enabled = true;
        owner.Agent.enabled = true;
        owner.Agent.speed = owner.MovementSpeed;
        owner.Agent.acceleration = owner.Acceleration;
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();
    }
}
