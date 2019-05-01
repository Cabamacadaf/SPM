using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy2/LeapRecoverState")]
public class EnemyLeapRecoverState : EnemyBaseState
{
    private Enemy2 owner2;
    private float timer;

    public override void Enter ()
    {
        Debug.Log("Leap Recover State");
        base.Enter();
        timer = 0.0f;
        owner2 = (Enemy2)owner;
        owner2.attackHitbox.SetActive(false);
        owner2.mouth.gameObject.SetActive(false);
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
}
