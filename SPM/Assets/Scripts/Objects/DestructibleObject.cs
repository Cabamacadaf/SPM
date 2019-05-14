//Author: Marcus Mellström

using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public float hitPoints = 20.0f;

    private void Update ()
    {
        if(hitPoints <= 0.0f) {
            Destroy(gameObject);
        }
    }
}
