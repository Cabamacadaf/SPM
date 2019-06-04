//Author: Marcus Mellström

using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField] private int id;
    private Transform respawnPoint;
    public bool reachedCheckpoint;

    public int ID { get => id; set => id = value; }

    private void OnEnable()
    {
        //LoadCheckpoint();
    }

    private void LoadCheckpoint()
    {
        //CheckpointData data = SaveSystem.LoadCheckpoint();
        //if (data != null)
        //{
        //    reachedCheckpoint = data.reachedCheckpoint;
        //}
        LevelManager.Instance.AllCheckpoints.Add(ID, this);
    }

    private void Start()
    {
        respawnPoint = GetComponent<Transform>().GetChild(0); 
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance != null && reachedCheckpoint == false) {
            GameManager.Instance.CurrentCheckPoint = respawnPoint.position;
            GameManager.Instance.SaveGame();
            reachedCheckpoint = true;
            //SaveSystem.SaveCheckpoint(this);
        }
    }
}
