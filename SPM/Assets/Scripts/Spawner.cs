//Author: Marcus Mellström

using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    public void Spawn ()
    {
        Instantiate(objectToSpawn, transform);
    }
}
