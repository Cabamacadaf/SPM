﻿//Author: Marcus Mellström

using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/Enemy1/IdleState")]
public class Enemy1IdleState : EnemyIdleState
{
    public override void HandleUpdate ()
    {
        base.HandleUpdate();
    }

	public override void Enter () 
	{
		owner.anim.SetFloat("Enemy1Speed", 0.0f);
	}
		
}