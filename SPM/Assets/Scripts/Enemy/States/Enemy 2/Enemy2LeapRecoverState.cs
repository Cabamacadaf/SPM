//Author: Marcus Mellström

using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/Enemy2/LeapRecoverState")]
public class Enemy2LeapRecoverState : EnemyBaseState
{
    private Enemy2 owner2;
    private float timer;

    public override void Initialize (StateMachine owner)
    {
        base.Initialize(owner);
        owner2 = (Enemy2)owner;
    }

    public override void Enter ()
    {
        //Debug.Log("Leap Recover State");
        base.Enter();
        Owner.RigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        timer = 0.0f;
        Owner.Animator.SetFloat("Enemy2Speed", 0.0f);
    }

    public override void HandleUpdate ()
    {
        Owner.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(Owner.transform.forward, Owner.Player.transform.position - Owner.transform.position, owner2.RotationSpeed * Time.deltaTime, 0.0f));

        timer += Time.deltaTime;

        if (timer >= owner2.LeapCooldown) {
            Owner.Transition<Enemy2AggroState>();
        }

        base.HandleUpdate();
    }

    public override void Exit ()
    {
        owner2.LeapAttackHitbox.SetActive(false);
        owner2.Mouth.gameObject.SetActive(false);
        Owner.RigidBody.constraints = RigidbodyConstraints.FreezeAll;
        base.Exit();
    }
}
