//Author: Simon Sundström
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    [SerializeField] private GameObject keyCard;

    public bool HasPowerCoreKeycard { get; set; }
    public bool HasExitKeycard { get; set; }
    public bool HasAllPowerCores { get; private set; }
    public int PowerCoreCounter { get; set; }

    public static ObjectiveController Instance;


    private void Awake()
    {
        Instance = this;

        PowerCoreCounter = 0;
        HasPowerCoreKeycard = false;
        HasExitKeycard = true;
        HasAllPowerCores = false;
    }

    public void AddPowerCore()
    {
        PowerCoreCounter++;

        if (PowerCoreCounter >= 4)
        {
            HasAllPowerCores = true;
            PowerCoreCounter = 0;
        }
    }
}
