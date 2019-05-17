//Author: Marcus Mellström

using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/Enemy2/IdleState")]
public class Enemy2IdleState : EnemyIdleState
{
	public override void Enter () 
	{
		owner.animator.SetFloat("Enemy2Speed", 0.0f);
	}
}
