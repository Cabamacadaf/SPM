using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/Enemy1/DeathState")]

public class EnemyDeathState : EnemyBaseState
{
    public override void Enter()
    {
        Owner.Animator.SetTrigger("Enemy1Death");
        EnemyDeathEvent enemyDeathEvent = new EnemyDeathEvent(Owner.gameObject);
        enemyDeathEvent.EventDescription = "Enemy " + Owner.gameObject.name + " has died.";
        enemyDeathEvent.ExecuteEvent();

    }
}
