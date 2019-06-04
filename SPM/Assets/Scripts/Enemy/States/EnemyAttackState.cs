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
        foreach (GameObject attackObject in Owner.AttackObjects) {
            attackObject.SetActive(true);
        }

        Owner.Animator.SetTrigger("EnemyAttack");

        base.Enter();
    }

    public override void HandleUpdate ()
    {
        if (AttackTimer >= Owner.AttackTime) {
            Owner.Transition<EnemyAttackRecoverState>();
        }

        AttackTimer += Time.deltaTime;
        base.HandleUpdate();
    }

    public override void Exit ()
    {
        base.Exit();
        Owner.HasAttacked = false;
        foreach(GameObject attackObject in Owner.AttackObjects) {
            attackObject.SetActive(false);
        }
    }
}
