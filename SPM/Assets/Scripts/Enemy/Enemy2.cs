using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : Enemy
{
    public float leapRange = 10.0f;
    public float leapCooldown = 1.0f;
    public float leapChargeTime = 0.5f;
    public float leapHeight = 5.0f;
    public float leapTime = 1.0f;

    protected override void Awake ()
    {
        base.Awake();
    }
}
