using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/IdleState")]
public class EnemyIdleState : EnemyBaseState
{
    [SerializeField] private float aggroDistance = 5.0f;

    public override void Enter ()
    {
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();
        //if (Vector3.Distance(owner.player.transform.position, owner.transform.position) <= aggroDistance) {
        //    owner.Transition<EnemyAggroState>();
        //}
    }
}
