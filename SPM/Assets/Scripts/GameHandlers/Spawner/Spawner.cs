//Author: Marcus Mellström

using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected GameObject objectToSpawn;
    public GameObject Spawn ()
    {
        return Instantiate(objectToSpawn, transform);
    }
}
