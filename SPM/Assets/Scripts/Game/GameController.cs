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
    public int PowerCoreCounter { get; set; }


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

        PowerCoreCounter = 0;
        HasLevel1Keycard = false;
        HasLevel2Keycard = true;
        HasAllPowerCores = false;
    }

    public void AddPowerCore()
    {
        PowerCoreCounter++;

        if(PowerCoreCounter >= 4)
        {
            //KeySpawn();
            HasAllPowerCores = true;
            PowerCoreCounter = 0;
        }
    }

    void KeySpawn ()
    {
        Instantiate(keyCard, keySpawnPoint.position, Quaternion.identity);
    }


}
