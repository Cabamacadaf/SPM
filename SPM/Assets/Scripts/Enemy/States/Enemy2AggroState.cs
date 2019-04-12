using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2AggroState : EnemyAggroState
{
    [SerializeField] private float chaseDistance = 6.0f;
    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();
        owner.transform.position += new Vector3(Mathf.PingPong(Time.time, 3), owner.transform.position.y, owner.transform.position.z);
        
        //MoveTowardPlayer();
    }
}
