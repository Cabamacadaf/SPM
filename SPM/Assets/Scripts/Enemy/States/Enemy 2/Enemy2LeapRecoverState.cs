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
        owner.rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        timer = 0.0f;
        owner2.leapAttackHitbox.SetActive(false);
        owner2.mouth.gameObject.SetActive(false);
		owner.animator.SetFloat("Enemy2Speed", 0.0f);
    }

    public override void HandleUpdate ()
    {
        owner.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(owner.transform.forward, owner.player.transform.position - owner.transform.position, owner2.rotationSpeed * Time.deltaTime, 0.0f));

        timer += Time.deltaTime;

        if (timer >= owner2.leapCooldown) {
            owner.Transition<Enemy2AggroState>();
        }

        base.HandleUpdate();
    }

    public override void Exit ()
    {
        owner.rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        base.Exit();
    }
}
