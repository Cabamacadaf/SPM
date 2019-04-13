using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy2/AggroState")]
public class Enemy2AggroState : EnemyAggroState
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();
        owner.transform.position += new Vector3(Mathf.PingPong(Time.time, 3), owner.transform.position.y, owner.transform.position.z);
    }
}
