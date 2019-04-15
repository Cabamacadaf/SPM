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
    public float leapRecovery = 0.5f;
    public float leapDamage = 10.0f;
    [HideInInspector] public Transform mouth;
    [HideInInspector] public BoxCollider mouthCollider;
    [HideInInspector] public MeshRenderer mouthRenderer;

    protected override void Awake ()
    {
        base.Awake();
        mouth = transform.GetChild(4);
        mouthCollider = mouth.GetComponent<BoxCollider>();
        mouthRenderer = mouth.GetComponent<MeshRenderer>();
    }
}
