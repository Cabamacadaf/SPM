using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSettings : MonoBehaviour
{
    public static UpgradeSettings instance;


    [SerializeField]
    private float maxChargingTime = 3;
    [SerializeField]
    private float growRate = 20;
    [SerializeField]
    private bool hasUpgrade;

    private void Awake()
    {
        instance = this;
    }

    public float MaxTime { get => maxChargingTime; set => maxChargingTime = value; }
    public float GrowRate { get => growRate; set => growRate = value; }
    public bool HasUpgrade { get => hasUpgrade; set => hasUpgrade = value; }
}
