//Author: Marcus Mellström

using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/Enemy2/LeapChargeState")]
public class Enemy2LeapChargeState : EnemyBaseState
{
    private float timer;
    private Enemy2 owner2;

    public override void Initialize (StateMachine owner)
    {
        base.Initialize(owner);
        owner2 = (Enemy2)owner;
    }

    public override void Enter ()
    {
        base.Enter();
        //Debug.Log("Leap Charge State");
        timer = 0.0f;
        owner2.Mouth.gameObject.SetActive(true);
		owner.Animator.SetTrigger("Enemy2JumpAttack");
    }
    public override void HandleUpdate ()
    {
        owner.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(owner.transform.forward, owner.Player.transform.position - owner.transform.position, owner.RotationSpeed * Time.deltaTime, 0.0f));

        timer += Time.deltaTime;
        if(timer >= owner2.LeapChargeTime) {
            owner.Transition<Enemy2LeapState>();
        }
        base.HandleUpdate();
    }
}
