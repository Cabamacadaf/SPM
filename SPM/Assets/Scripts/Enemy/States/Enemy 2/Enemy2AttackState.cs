﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy2/AttackState")]
public class Enemy2AttackState : EnemyAttackState
{
    public override void Enter ()
    {
        Debug.Log("Attack State");
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();
    }
}