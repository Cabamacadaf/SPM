//Author: Marcus Mellström

using System.Collections;
using UnityEngine;

public class HeavyDoor : Door
{
    [SerializeField] private float doorSpeed = 1.0f;
    [SerializeField] private Transform leftDoor;
    [SerializeField] private Transform rightDoor;

    private Vector3 leftMaxDistance = new Vector3(-1.5f, 0, 0);
    private Vector3 rightMaxDistance = new Vector3(1.5f, 0, 0);

    private bool isMoving = false;

    private void Awake ()
    {
        if (closed) {
            leftDoor.localPosition = Vector3.zero;
            rightDoor.localPosition = Vector3.zero;
        }
        else if (open) {
            leftDoor.localPosition = leftMaxDistance;
            rightDoor.localPosition = rightMaxDistance;
        }
    }

    public override void Open ()
    {
        closed = false;
        StartCoroutine(Opening());
    }

    public override void Close ()
    {
        open = false;
        StartCoroutine(Closing());
    }

    private IEnumerator Opening ()
    {
        if (isMoving) {
            yield break;
        }
        isMoving = true;

        float timer = 0.0f;

        while (rightDoor.localPosition.x < rightMaxDistance.x) {
            timer += Time.deltaTime;
            leftDoor.localPosition = Vector3.Lerp(Vector3.zero, leftMaxDistance, doorSpeed * timer);
            rightDoor.localPosition = Vector3.Lerp(Vector3.zero, rightMaxDistance, doorSpeed * timer);
            yield return null;
        }
        open = true;
        isMoving = false;
    }

    private IEnumerator Closing ()
    {
        if (isMoving) {
            yield break;
        }
        isMoving = true;

        float timer = 0.0f;

        while (rightDoor.localPosition.x > 0) {
            timer += Time.deltaTime;
            leftDoor.localPosition = Vector3.Lerp(leftMaxDistance, Vector3.zero, doorSpeed * timer);
            rightDoor.localPosition = Vector3.Lerp(rightMaxDistance, Vector3.zero, doorSpeed * timer);
            yield return null;
        }
        closed = true;
        isMoving = false;
    }
}
