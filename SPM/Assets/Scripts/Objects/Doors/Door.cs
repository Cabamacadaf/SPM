//Author: Marcus Mellström

using UnityEngine;

public abstract class Door : MonoBehaviour
{
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private bool isOpen;
    [SerializeField] private bool isClosed;

    protected AudioClip DoorSound { get => doorSound; set => doorSound = value; }
    protected AudioSource AudioSource { get; set; }

    public bool IsOpen { get => isOpen; set => isOpen = value; }
    public bool IsClosed { get => isClosed; set => isClosed = value; }

    public abstract void Open ();
    public abstract void Close ();

    private void Awake ()
    {
        AudioSource = GetComponent<AudioSource>();
    }
}
