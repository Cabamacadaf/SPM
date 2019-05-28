using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    [SerializeField] private GameObject keyCard;
    [SerializeField] private Transform keySpawnPoint;


    public bool HasLevel1Keycard { get; set; }
    public bool HasLevel2Keycard { get; set; }
    public bool HasAllPowerCores { get; private set; }

    private int powerCoreCounter;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }

        else
        {
            Destroy(gameObject);
        }

        powerCoreCounter = 0;
        HasLevel1Keycard = false;
        HasLevel2Keycard = true;
        HasAllPowerCores = false;
    }

    public void AddPowerCore()
    {
        powerCoreCounter++;

        if(powerCoreCounter >= 4)
        {
            //KeySpawn();
            HasAllPowerCores = true;
            powerCoreCounter = 0;
        }
    }

    void KeySpawn ()
    {
        Instantiate(keyCard, keySpawnPoint.position, Quaternion.identity);
    }
}
