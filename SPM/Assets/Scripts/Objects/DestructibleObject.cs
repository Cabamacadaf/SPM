//Author: Marcus Mellström

using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    [SerializeField] private int iD;
    [SerializeField] private float startHitPoints = 20.0f;

    public int ID { get => iD; set => iD = value; }
    public float StartHitPoints { get => startHitPoints; set => startHitPoints = value; }
    public float HitPoints { get; set; }

    private void Awake ()
    {
        HitPoints = startHitPoints;
        LevelManager.Instance.AllDestructibleObjects.Add(ID, this);
    }

    private void Update ()
    {
        if(HitPoints <= 0.0f) {
            gameObject.SetActive(false);
        }
    }
}
