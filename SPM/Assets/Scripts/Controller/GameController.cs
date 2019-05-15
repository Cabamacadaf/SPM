using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    [SerializeField] private GameObject keyCard;
    [SerializeField] private Transform keySpawnPoint;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject[] lights;

    public bool HasLevel1Keycard { get; set; }
    public bool HasLevel2Keycard { get; set; }

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
    }

    public void AddPowerCore()
    {
        powerCoreCounter++;

        if(powerCoreCounter == 4)
        {
            KeySpawn();
            powerCoreCounter = 0;
        }
    }

    public void AddLastPuzzle()
    {
        if (lastPuzzleCounter == 9)
        {
            Debug.Log("You have reached your goal");
            OpenDoor();
            lastPuzzleCounter = 0;
            SetAllLightTrue();
        }
    }

    void KeySpawn ()
    {
        Instantiate(keyCard, keySpawnPoint.position, Quaternion.identity);
    }


    void OpenDoor()
    {
        door.GetComponent<SplittingDoor>().Open();
        Debug.Log("The path is open");
        return;
    }

    private void SetAllLightFalse()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(false);
        }
    }

    private void SetAllLightTrue()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
        }
    }
}
