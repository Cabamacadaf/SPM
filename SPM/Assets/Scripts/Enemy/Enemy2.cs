//Author: Marcus Mellström

using UnityEngine;

public class Enemy2 : Enemy
{
    #region Private Fields
    [SerializeField] private float damageReduction = 0.17f;

    [SerializeField] private float leapDamage = 50.0f;
    [SerializeField] private float leapSpeed = 1.5f;
    [SerializeField] private float leapHeight = 3.0f;
    [SerializeField] private float leapTime = 1.0f;

    [SerializeField] private float minLeapRange = 5.0f;
    [SerializeField] private float maxLeapRange = 10.0f;

    [SerializeField] private float leapChargeTime = 1.0f;
    [SerializeField] private float leapCooldown = 1.0f;

    [SerializeField] private Transform mouth;
    [SerializeField] private GameObject leapAttackHitbox;

    [SerializeField] private AudioClip leapSound;
    #endregion

    #region Properties
    public float DamageReduction { get => damageReduction; private set => damageReduction = value; }

    public float LeapDamage { get => leapDamage; private set => leapDamage = value; }
    public float LeapSpeed { get => leapSpeed; private set => leapSpeed = value; }
    public float LeapHeight { get => leapHeight; private set => leapHeight = value; }
    public float LeapTime { get => leapTime; private set => leapTime = value; }

    public float MinLeapRange { get => minLeapRange; private set => minLeapRange = value; }
    public float MaxLeapRange { get => maxLeapRange; private set => maxLeapRange = value; }

    public float LeapChargeTime { get => leapChargeTime; private set => leapChargeTime = value; }
    public float LeapCooldown { get => leapCooldown; private set => leapCooldown = value; }

    public Transform Mouth { get => mouth; private set => mouth = value; }
    public GameObject LeapAttackHitbox { get => leapAttackHitbox; private set => leapAttackHitbox = value; }

    public AudioClip LeapSound { get => leapSound; private set => leapSound = value; }
    #endregion
}
