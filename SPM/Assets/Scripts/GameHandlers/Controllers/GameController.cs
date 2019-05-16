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
    public bool Level2PuzzleComplete { get; private set; }

    private int powerCoreCounter;
    private int lastPuzzleCounter;


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
    }

    void Start()
    {
        powerCoreCounter = 0;
        HasLevel1Keycard = false;
        lastPuzzleCounter = 0;
        HasLevel2Keycard = true;
        HasAllPowerCores = false;
        Level2PuzzleComplete = false;
    }

    public void AddPowerCore()
    {
        powerCoreCounter++;

        if(powerCoreCounter == 4)
        {
            //KeySpawn();
            HasAllPowerCores = true;
            powerCoreCounter = 0;
        }
    }

    public void AddLastPuzzle()
    {
        lastPuzzleCounter++;
        if (lastPuzzleCounter >= 7)
        {
            Debug.Log("You have reached your goal");
            
            lastPuzzleCounter = 0;
            Level2PuzzleComplete = true;
        }
    }

    void KeySpawn ()
    {
        Instantiate(keyCard, keySpawnPoint.position, Quaternion.identity);
    }
}
