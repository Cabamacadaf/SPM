//Author: Marcus Mellström

public class EnemyIdleState : EnemyBaseState
{
    float rotationY = 0;

    public override void Enter ()
    {
        //Debug.Log("Idle State");
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        //rotationY += owner.transform.rotation.y + 2.0f * Time.deltaTime;
        //owner.transform.rotation = Quaternion.Euler(rotationY, 0, 0);
        base.HandleUpdate();
    }
}
