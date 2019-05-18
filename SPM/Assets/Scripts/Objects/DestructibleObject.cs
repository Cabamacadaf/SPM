//Author: Marcus Mellström

using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    [SerializeField] private float hitPoints = 20.0f;
    public float HitPoints { get => hitPoints; set => hitPoints = value; }

    private void Update ()
    {
        if(HitPoints <= 0.0f) {
            Destroy(gameObject);
        }
    }
}
