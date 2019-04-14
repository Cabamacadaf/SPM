using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy2/LeapState")]
public class EnemyLeapState : EnemyBaseState
{
    private float chargeTimer;
    private float leapTimer;
    private bool leaping;
    private Enemy2 owner2;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 midPosition;

    public override void Enter ()
    {
        leaping = false;
        owner2 = (Enemy2)owner;
        chargeTimer = 0.0f;
        leapTimer = 0.0f;
        Debug.Log("Leap");
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        chargeTimer += Time.deltaTime;
        if(!leaping && chargeTimer >= owner2.leapChargeTime) {
            Debug.Log("Charging");
            startPosition = owner.transform.position;
            endPosition = owner.player.transform.position;
            midPosition = startPosition + (endPosition - startPosition) / 2 + Vector3.up * owner2.leapHeight;
            leaping = true;
        }

        if (leaping && leapTimer < owner2.leapTime) {
            Debug.Log("Leaping");
            leapTimer += Time.deltaTime;
            Vector3 m1 = Vector3.Lerp(startPosition, midPosition, leapTimer);
            Vector3 m2 = Vector3.Lerp(midPosition, endPosition, leapTimer);
            owner.transform.position = Vector3.Lerp(m1, m2, leapTimer);
        }

        if(leapTimer >= owner2.leapTime) {
            owner.Transition<Enemy2AggroState>();
        }
    }
}
