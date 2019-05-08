//Author: Marcus Mellström

using UnityEngine;

public abstract class Door : MonoBehaviour
{
    public bool open;
    public bool closed;

    public abstract void Open ();
    public abstract void Close ();
}
