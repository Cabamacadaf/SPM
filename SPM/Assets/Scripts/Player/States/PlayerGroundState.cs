﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "States/Player/GroundState")]

public class PlayerGroundState : PlayerBaseState
{
    // Attributes
    protected int stamina = 1000;
    // Methods
    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
        Debug.Log("Stamina: " + owner.Movement.stamina);

        if (Input.GetKeyDown(KeyCode.Space))
        {

            owner.Movement.AddVelocity(Vector2.up * owner.Movement.JumpHeight);
            owner.Transition<PlayerAirState>();

        }
        base.HandleUpdate();
        RaycastHit hitInfo;
        if (Physics.Raycast(owner.transform.position, Vector3.down, out hitInfo))
        {
            direction = Vector3.ProjectOnPlane(direction, hitInfo.normal).normalized;

        }


    }
}
