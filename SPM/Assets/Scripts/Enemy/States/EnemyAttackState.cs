//Author: Marcus Mellström

using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/AttackState")]
public class EnemyAttackState : EnemyBaseState
{
    protected float AttackTimer { get; set; }

    public override void Enter ()
    {
        //Debug.Log("Attack State");
        AttackTimer = 0.0f;
        EnemyAttackEvent enemyAttackEvent = new EnemyAttackEvent(Owner.AttackSound, Owner.AudioSource);
        enemyAttackEvent.ExecuteEvent();
        Owner.AttackObject.SetActive(true);

		if (Owner is Enemy1) {
			Owner.Animator.SetTrigger("Enemy1Attack");
		} else if (Owner is Enemy2) {
			Owner.Animator.SetTrigger("Enemy2Attack");
		}

        base.Enter();
    }

    public override void HandleUpdate ()
    {
        if (AttackTimer >= Owner.AttackTime) {
            Owner.Transition<EnemyAttackRecoverState>();
        }

        Owner.AttackObject.transform.position += Owner.AttackObject.transform.forward * Time.deltaTime * Owner.AttackAnimationSpeed;
        AttackTimer += Time.deltaTime;
        base.HandleUpdate();
    }

    public override void Exit ()
    {
        base.Exit();
        Owner.AttackObject.transform.position = Owner.transform.position;
        Owner.GetComponentInChildren<Attack>().hasAttacked = false;
        Owner.AttackObject.SetActive(false);
    }
}
