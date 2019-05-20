using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manipulate : MonoBehaviour
{
    public bool IsActive { get; set; }

    [SerializeField] private Transform endObject;
    private Vector3 endPosition;
    private Vector3 startPosition;

    [SerializeField] private float speed;
    [SerializeField] private float returnSpeed;

    [SerializeField] private bool canReturn;

    private bool reachEndPoint;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        endPosition = endObject.position;



    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("ISACTIVE: " + IsActive);
        if (IsActive)
        {
           
            if (reachEndPoint && canReturn)
            {
                Debug.Log("Move to Start");
                MoveToStartPosition();
            }
            else
            {
                Debug.Log("Move TO end");
                MoveToEndPosition();
            }
        }
    }

    private void MoveToEndPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
        if(transform.position == endPosition)
        {
            reachEndPoint = true;
            if(canReturn == false)
            {
                IsActive = false;
            }

        }
    }

    private void MoveToStartPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPosition, returnSpeed * Time.deltaTime);
        if (transform.position == startPosition)
        {
            IsActive = false;
            reachEndPoint = false;

        }
    }


}



[System.Serializable]
public enum MovingAxes
{
    X, Y, Z
}
