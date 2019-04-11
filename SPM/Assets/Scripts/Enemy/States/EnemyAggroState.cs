using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/AggroState")]
public class EnemyAggroState : EnemyBaseState
{
    public override void HandleUpdate ()
    {
        owner.agent.SetDestination(owner.player.transform.position);
        base.HandleUpdate();
        //MoveTowardPlayer();
    }

    //private void MoveTowardPlayer ()
    //{
    //    Vector3 direction = (player.position - transform.position).normalized;
    //    direction = new Vector3(direction.x, 0, direction.z).normalized;
    //    transform.position += direction * movementSpeed * Time.deltaTime;
    //}
}
