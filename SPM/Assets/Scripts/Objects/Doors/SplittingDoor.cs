//Author: Marcus Mellström

using System.Collections;
using UnityEngine;

public class SplittingDoor : Door
{
    [SerializeField] private float doorSpeed = 1.0f;
    [SerializeField] private Transform leftDoor;
    [SerializeField] private Transform rightDoor;
    [SerializeField] private Transform topDoor;

    private Vector3 leftMaxDistance = new Vector3(1.6f, 0, 0);
    private Vector3 rightMaxDistance = new Vector3(-1.6f, 0, 0);
    private Vector3 topMaxDistance = new Vector3(0, 1.9f, 0);

    private bool isMoving = false;

    private void Awake ()
    {
        if (IsClosed) {
            leftDoor.localPosition = Vector3.zero;
            rightDoor.localPosition = Vector3.zero;
            topDoor.localPosition = Vector3.zero;
        }
        else if (IsOpen) {
            leftDoor.localPosition = leftMaxDistance;
            rightDoor.localPosition = rightMaxDistance;
            topDoor.localPosition = topMaxDistance;
        }
    }

    public override void Open ()
    {
        IsClosed = false;
        StartCoroutine(Opening());
    }

    public override void Close ()
    {
        IsOpen = false;
        StartCoroutine(Closing());
    }

    private IEnumerator Opening ()
    {
        if (isMoving) {
            yield break;
        }
        isMoving = true;

        float timer = 0.0f;

        while (topDoor.localPosition.y < topMaxDistance.y) {
            timer += Time.deltaTime;
            leftDoor.localPosition = Vector3.Lerp(Vector3.zero, leftMaxDistance, doorSpeed * timer);
            rightDoor.localPosition = Vector3.Lerp(Vector3.zero, rightMaxDistance, doorSpeed * timer);
            topDoor.localPosition = Vector3.Lerp(Vector3.zero, topMaxDistance, doorSpeed * timer);
            yield return null;
        }
        IsOpen = true;
        isMoving = false;
    }

    private IEnumerator Closing ()
    {
        if (isMoving) {
            yield break;
        }
        isMoving = true;

        float timer = 0.0f;

        while (topDoor.localPosition.y > 0) {
            timer += Time.deltaTime;
            leftDoor.localPosition = Vector3.Lerp(leftMaxDistance, Vector3.zero, doorSpeed * timer);
            rightDoor.localPosition = Vector3.Lerp(rightMaxDistance, Vector3.zero, doorSpeed * timer);
            topDoor.localPosition = Vector3.Lerp(topMaxDistance, Vector3.zero, doorSpeed * timer);
            yield return null;
        }
        IsClosed = true;
        isMoving = false;
    }
}
