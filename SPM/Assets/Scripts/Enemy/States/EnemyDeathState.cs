using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/Enemy1/DeathState")]

public class EnemyDeathState : EnemyBaseState
{
    private float timer = 0.0f;
    private float deathAnimationTime = 2.0f;

    public override void Enter()
    {
        Owner.Animator.SetTrigger("EnemyDeath");
        Owner.AudioSource.PlayOneShot(Owner.DeathSound);
        timer = 0.0f;
    }

    public override void HandleUpdate ()
    {
        timer += Time.deltaTime;
        if (timer >= deathAnimationTime) {
            timer = 0.0f;
            Owner.gameObject.SetActive(false);
        }
        base.HandleUpdate();
    }
}
