//Author: Marcus Mellström

using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    protected GameObject ObjectToSpawn { get => objectToSpawn; private set => objectToSpawn = value; }
    
    

    public GameObject Spawn ()
    {
        GameObject spawnedObject = Instantiate(ObjectToSpawn, transform);
        LevelManager.instance.AllPickUpObjects.Add(spawnedObject.GetInstanceID(), spawnedObject);
        return spawnedObject;

    }
}
