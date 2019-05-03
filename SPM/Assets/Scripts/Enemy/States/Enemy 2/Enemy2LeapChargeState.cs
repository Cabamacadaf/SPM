using System.Collections;
using System.Collections.Generic;
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
        owner2.mouth.gameObject.SetActive(true);
    }
    public override void HandleUpdate ()
    {
        owner.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(owner.transform.forward, owner.player.transform.position - owner.transform.position, owner.rotationSpeed * Time.deltaTime, 0.0f));

        timer += Time.deltaTime;
        if(timer >= owner2.leapChargeTime) {
            owner.Transition<Enemy2LeapState>();
        }
        base.HandleUpdate();
    }
}
