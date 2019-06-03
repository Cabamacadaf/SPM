//Author: Marcus Mellström

using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    [SerializeField] private int iD;
    [SerializeField] private float hitPoints = 20.0f;

    public int ID { get => iD; set => iD = value; }
    public float HitPoints { get => hitPoints; set => hitPoints = value; }

    private void Awake ()
    {
        LevelManager.Instance.AllDestructibleObjects.Add(ID, gameObject);
    }

    private void Update ()
    {
        if(HitPoints <= 0.0f) {
            gameObject.SetActive(false);
        }
    }
}
