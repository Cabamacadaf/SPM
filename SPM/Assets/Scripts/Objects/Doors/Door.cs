//Author: Marcus Mellström

using UnityEngine;

public abstract class Door : MonoBehaviour
{
    [SerializeField] private bool isOpen;
    [SerializeField] private bool isClosed;

    public bool IsOpen { get => isOpen; set => isOpen = value; }
    public bool IsClosed { get => isClosed; set => isClosed = value; }

    public abstract void Open ();
    public abstract void Close ();
}
