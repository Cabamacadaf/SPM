//Author: Marcus Mellström

using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/AttackRecoverState")]
public class EnemyAttackRecoverState : EnemyBaseState
{
    private float timer;

    public override void Enter ()
    {
        //Debug.Log("Attack Recover State");
        timer = 0.0f;
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        Owner.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(Owner.transform.forward, Owner.Player.transform.position - Owner.transform.position, Owner.RotationSpeed * Time.deltaTime, 0.0f));

        timer += Time.deltaTime;

        if (timer >= Owner.AttackCooldown) {
            if (Owner is Enemy1) {
                Owner.Transition<Enemy1AggroState>();
            }
            else if (Owner is Enemy2) {
                Owner.Transition<Enemy2AggroState>();
            }
        }
        base.HandleUpdate();
    }
}
