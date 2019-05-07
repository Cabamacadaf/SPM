using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : Enemy
{
    public AudioClip leapSound;

    public float maxLeapRange = 40.0f;
    public float minLeapRange = 20.0f;
    public float leapChargeTime = 1.0f;
    public float leapHeight = 5.0f;
    public float leapTime = 1.0f;
    public float leapCooldown = 1.0f;
    public float leapDamage = 50.0f;
    public Transform mouth;
    public GameObject leapAttackHitbox;
    public float leapSpeed = 2.0f;
    public float damageReduction = 0.5f;
}
