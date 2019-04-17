using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy2/LeapState")]
public class EnemyLeapState : EnemyBaseState
{
    private float chargeTimer;
    private float leapTimer;
    private bool leaping;
    private bool hit;
    private bool lept;
    private Enemy2 owner2;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 midPosition;

    public override void Enter ()
    {
        Debug.Log("Leap State");
        hit = false;
        leaping = false;
        lept = false;
        owner2 = (Enemy2)owner;
        owner2.mouthRenderer.enabled = true;
        owner2.mouthCollider.enabled = true;
        chargeTimer = 0.0f;
        leapTimer = 0.0f;
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();
        chargeTimer += Time.deltaTime;
        if (!leaping && chargeTimer >= owner2.leapChargeTime) {
            hit = false;
            startPosition = owner.transform.position;
            endPosition = owner.player.transform.position;
            midPosition = startPosition + (endPosition - startPosition) / 2 + Vector3.up * owner2.leapHeight;
            owner2.attacking = true;
            leaping = true;
        }

        if (leaping) {
            leapTimer += Time.deltaTime;
            if (leapTimer < owner2.leapTime && !hit) {
                Vector3 m1 = Vector3.Lerp(startPosition, midPosition, leapTimer);
                Vector3 m2 = Vector3.Lerp(midPosition, endPosition, leapTimer);
                owner.transform.position = Vector3.Lerp(m1, m2, leapTimer);
            }

            if (leapTimer >= owner2.leapTime && !lept) {
                owner2.attacking = false;
                lept = true ;
                owner2.mouthCollider.enabled = false;
                owner2.mouthRenderer.enabled = false;
            }

            if (leapTimer >= owner2.leapTime + owner2.leapRecovery) {
                owner.Transition<Enemy2AggroState>();
            }
        }
    }

    public override void HandleCollision (Collision collision)
    {
        base.HandleCollision(collision);
        // hit = true;
        if (collision.collider.CompareTag("Player")) {
            Debug.Log("Enemy 2 Collision with Player");
        }

    }
}
