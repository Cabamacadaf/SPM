//Author: Marcus Mellström

using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/AttackState")]
public class EnemyAttackState : EnemyBaseState
{
    protected float timer = 0.0f;

    public override void Enter ()
    {
        //Debug.Log("Attack State");
        timer = 0.0f;
        EnemyAttackEvent enemyAttackEvent = new EnemyAttackEvent(owner.AttackSound, owner.AudioSource);
        enemyAttackEvent.ExecuteEvent();
        owner.AttackObject.SetActive(true);

		if (owner is Enemy1) {
			owner.Animator.SetTrigger("Enemy1Attack");
		} else if (owner is Enemy2) {
			owner.Animator.SetTrigger("Enemy2Attack");
		}

        base.Enter();
    }

    public override void HandleUpdate ()
    {
        if (timer >= owner.AttackTime) {
            owner.Transition<EnemyAttackRecoverState>();
        }

        owner.AttackObject.transform.position += owner.AttackObject.transform.forward * Time.deltaTime * owner.AttackAnimationSpeed;
        timer += Time.deltaTime;
        base.HandleUpdate();
    }

    public override void Exit ()
    {
        base.Exit();
        owner.AttackObject.transform.position = owner.transform.position;
        owner.GetComponentInChildren<Attack>().hasAttacked = false;
        owner.AttackObject.SetActive(false);
    }
}
