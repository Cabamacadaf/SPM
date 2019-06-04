using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;

    public List<GameObject> ActiveObjects = new List<GameObject>();
    public Dictionary<int, GameObject> AllPickUpObjects = new Dictionary<int, GameObject>();
    public Dictionary<int, DestructibleObject> AllDestructibleObjects = new Dictionary<int, DestructibleObject>();
    public Dictionary<int, Enemy> AllEnemies = new Dictionary<int, Enemy>();
    public Dictionary<int, HealthPack> AllHealthPacks = new Dictionary<int, HealthPack>();
    public Dictionary<int, CheckpointTrigger> AllCheckpoints = new Dictionary<int, CheckpointTrigger>();

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1;
    }

    public void Save()
    {
        SaveSystem.SaveObjects(AllPickUpObjects);
        SaveSystem.SaveEnemies(AllEnemies);
        SaveSystem.SaveDestructibleObjects(AllDestructibleObjects);
        SaveSystem.SaveHealthPack(AllHealthPacks);
    }

}
