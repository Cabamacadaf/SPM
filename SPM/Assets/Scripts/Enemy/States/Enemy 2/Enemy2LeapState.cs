//Author: Marcus Mellström

using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/Enemy2/LeapState")]
public class Enemy2LeapState : EnemyBaseState
{
    private float timer;
    private Enemy2 owner2;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 midPosition;

    public override void Enter ()
    {
        //Debug.Log("Leap State");
        owner2 = (Enemy2)Owner;
        timer = 0.0f;
        startPosition = Owner.transform.position;
        endPosition = Owner.Player.transform.position;
        midPosition = startPosition + (endPosition - startPosition) / 2 + Vector3.up * owner2.LeapHeight;
        owner2.LeapAttackHitbox.SetActive(true);
        EnemyAttackEvent enemyAttackEvent = new EnemyAttackEvent(owner2.LeapSound, Owner.AudioSource);
        enemyAttackEvent.ExecuteEvent();
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        timer += Time.deltaTime * owner2.LeapSpeed;

        Vector3 m1 = Vector3.Lerp(startPosition, midPosition, timer);
        Vector3 m2 = Vector3.Lerp(midPosition, endPosition, timer);
        Owner.transform.position = Vector3.Lerp(m1, m2, timer);

        if (timer >= owner2.LeapTime) {
            Owner.Transition<Enemy2LeapRecoverState>();
        }
        base.HandleUpdate();
    }

    public override void HandleCollision (Collision collision)
    {
        base.HandleCollision(collision);
        Owner.Transition<Enemy2LeapRecoverState>();
    }

    public override void Exit ()
    {
        base.Exit();
        Owner.GetComponentInChildren<Attack>().hasAttacked = false;
    }
}
