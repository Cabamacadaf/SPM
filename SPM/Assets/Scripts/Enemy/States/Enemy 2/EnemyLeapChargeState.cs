using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy2/LeapChargeState")]
public class EnemyLeapChargeState : EnemyBaseState
{
    private float timer;
    private Enemy2 owner2;
    public override void Enter ()
    {
        base.Enter();
        Debug.Log("Leap Charge State");
        timer = 0.0f;
        owner2 = (Enemy2)owner;
        owner2.mouthRenderer.enabled = true;
        owner2.mouthCollider.enabled = true;
    }
    public override void HandleUpdate ()
    {
        base.HandleUpdate();

        owner.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(owner.transform.forward, owner.player.transform.position - owner.transform.position, owner.rotationSpeed * Time.deltaTime, 0.0f));

        timer += Time.deltaTime;
        if(timer >= owner2.leapChargeTime) {
            owner.Transition<EnemyLeapState>();
        }
    }
}
