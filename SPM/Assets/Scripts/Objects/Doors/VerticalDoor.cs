//Author: Marcus Mellström

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalDoor : Door
{
    [SerializeField] private float doorSpeed = 1.0f;
    [SerializeField] private Transform topDoor;
    [SerializeField] private Transform bottomDoor;

    private Vector3 topDoorStartPosition = new Vector3(-1.963001f, 3.88f, 0);
    private Vector3 bottomDoorStartPosition = new Vector3(-1.957317f, 0, 0);

    private Vector3 topMaxDistance = new Vector3(-1.957317f, 5.9f, 0);
    private Vector3 bottomMaxDistance = new Vector3(-1.963001f, -2.33f, 0);

    private bool isOpening = false;
    private bool isClosing = false;

    private float timer;

    private void Awake ()
    {
        if (IsClosed) {
            topDoor.localPosition = topDoorStartPosition;
            bottomDoor.localPosition = bottomDoorStartPosition;
        }
        else if (IsOpen) {
            topDoor.localPosition = topMaxDistance;
            bottomDoor.localPosition = bottomMaxDistance;
        }
    }

    private void Update ()
    {
        if(isOpening == true) {
            timer += Time.deltaTime;
            if (bottomDoor.localPosition.y > bottomMaxDistance.y) {
                topDoor.localPosition = Vector3.Lerp(topDoorStartPosition, topMaxDistance, doorSpeed * timer);
                bottomDoor.localPosition = Vector3.Lerp(bottomDoorStartPosition, bottomMaxDistance, doorSpeed * timer);
            }
            else {
                timer = 0.0f;
                IsOpen = true;
                isOpening = false;
            }
        }

        if(isClosing == true) {
            timer += Time.deltaTime;
            if (bottomDoor.localPosition.y < bottomDoorStartPosition.y) {
                topDoor.localPosition = Vector3.Lerp(topMaxDistance, topDoorStartPosition, doorSpeed * timer);
                bottomDoor.localPosition = Vector3.Lerp(bottomMaxDistance, bottomDoorStartPosition, doorSpeed * timer);
            }
            else {
                timer = 0.0f;
                IsClosed = true;
                isClosing = false;
            }
        }
    }

    public override void Open ()
    {
        AudioSource.PlayOneShot(DoorSound);
        IsClosed = false;
        isOpening = true;
    }

    public override void Close ()
    {
        AudioSource.PlayOneShot(DoorSound);
        IsOpen = false;
        isClosing = true;
    }
}
