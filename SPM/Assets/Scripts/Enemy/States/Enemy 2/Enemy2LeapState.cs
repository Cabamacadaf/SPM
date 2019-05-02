using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy2/LeapState")]
public class Enemy2LeapState : EnemyBaseState
{
    private float timer;
    private Enemy2 owner2;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 midPosition;

    private bool hitPlayer;

    public override void Enter ()
    {
        hitPlayer = false;
        owner.audioSource.PlayOneShot(owner.attackSound);
        //Debug.Log("Leap State");
        owner2 = (Enemy2)owner;
        timer = 0.0f;
        startPosition = owner.transform.position;
        endPosition = owner.player.transform.position;
        midPosition = startPosition + (endPosition - startPosition) / 2 + Vector3.up * owner2.leapHeight;
        owner2.attackHitbox.SetActive(true);
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        timer += Time.deltaTime * owner2.leapSpeed;

        if (!hitPlayer) {
            Vector3 m1 = Vector3.Lerp(startPosition, midPosition, timer);
            Vector3 m2 = Vector3.Lerp(midPosition, endPosition, timer);
            owner.transform.position = Vector3.Lerp(m1, m2, timer);
        }

        if (timer >= owner2.leapTime) {
            owner.Transition<Enemy2LeapRecoverState>();
        }
        base.HandleUpdate();
    }

    public override void HandleCollision (Collision collision)
    {
        base.HandleCollision(collision);
        if (collision.collider.CompareTag("Player")) {
            hitPlayer = true;
        }
    }
    public override void Exit ()
    {
        base.Exit();
        owner.GetComponentInChildren<Attack>().hasAttacked = false;
    }
}
