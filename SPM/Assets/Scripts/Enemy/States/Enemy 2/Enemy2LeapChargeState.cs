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
		Owner.Animator.SetTrigger("Enemy2JumpAttack");
    }
    public override void HandleUpdate ()
    {
        Owner.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(Owner.transform.forward, Owner.Player.transform.position - Owner.transform.position, Owner.RotationSpeed * Time.deltaTime, 0.0f));

        timer += Time.deltaTime;
        if(timer >= owner2.LeapChargeTime) {
            Owner.Transition<Enemy2LeapState>();
        }
        base.HandleUpdate();
    }
}
