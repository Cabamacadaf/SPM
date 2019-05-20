using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    //Attributes
    public bool IsActive { get; set; }

    public Transform EndPosition;
    [HideInInspector] public GameObject Player;
    public MovingAxes movingAxes;


    [Tooltip("Ange ett värde mellan 0-1 för att ändra hur snabbt platformen rör sig med musen. \n" +
        "1: Rör sig exakt lika snabbt som musen \n" +
        "0: Rör sig inte alls")]
    [SerializeField] private float SmootherMovementValue;

    private Vector3 startPosition;
    private float maxDistance;
    private bool endPosIsLeftOfStart;
    private delegate Vector3 GetTargetPosition();
    private GetTargetPosition getTargetPosition;
    private delegate bool CanMove(Vector3 targetPosition);
    private CanMove canMove;

    public float maxDistanceDelta;
    private bool locked;


    //Methods
    void Start()
    {
        Player = GameObject.Find("Player");
        startPosition = transform.position;
        maxDistance = Vector3.Distance(startPosition, EndPosition.position);



        DelegateFuntions();

    }



    // Update is called once per frame
    void Update()
    {
        if (IsActive)
        {

            Vector3 targetPosition = getTargetPosition();


            //if (canMove(targetPosition))
            //{
            //    Move(targetPosition);
            //    //OnMouseDrag();
            //}
            Move(targetPosition);

        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    IsActive = false;
        //}

    }


    private void Move(Vector3 targetPosition)
    {
        Vector3 worldPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 point = Camera.main.ScreenToWorldPoint(
    new Vector3(
        Input.mousePosition.x,
        (transform.position.y - Camera.main.transform.position.y),
        (Vector3.Distance(transform.position, Camera.main.transform.position))));

        point.y = transform.position.y;
        point.z = transform.position.z;

        transform.position = Vector3.MoveTowards(transform.position, point, maxDistanceDelta);
    }

    private Vector3 GetTargetPositionX()
    {
        Vector3 targetPosition;
        float playerZPos = Player.transform.position.z;

        if (playerZPos < transform.position.z)
        {
            targetPosition = new Vector3(transform.position.x + Input.GetAxis("Mouse X"), transform.position.y, transform.position.z);

        }
        else
        {
            targetPosition = new Vector3(transform.position.x - Input.GetAxis("Mouse X"), transform.position.y, transform.position.z);

        }

        return targetPosition;
    }

    private Vector3 GetTargetPositionZ()
    {
        Vector3 targetPosition;
        float playerXPos = Player.transform.position.x;

        if (playerXPos < transform.position.x)
        {
            targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - Input.GetAxis("Mouse X"));

        }
        else
        {
            targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + Input.GetAxis("Mouse X"));

        }

        return targetPosition;
    }

    private Vector3 GetTargetPositionY()
    {

        return new Vector3(transform.position.x, transform.position.y + Input.GetAxis("Mouse Y"), transform.position.z);
    }



    private bool CanMoveX(Vector3 targetPosition)
    {
        if (endPosIsLeftOfStart)
        {

            return targetPosition.x < startPosition.x + maxDistance && targetPosition.x > startPosition.x;

        }
        else
        {
            return targetPosition.x > startPosition.x - maxDistance && targetPosition.x < startPosition.x;
        }
    }

    private bool CanMoveZ(Vector3 targetPosition)
    {
        if (endPosIsLeftOfStart)
        {

            return targetPosition.z < startPosition.z + maxDistance && targetPosition.z > startPosition.z;

        }
        else
        {
            return targetPosition.z > startPosition.z - maxDistance && targetPosition.z < startPosition.z;
        }
    }

    private bool CanMoveY(Vector3 targetPosition)
    {
        return targetPosition.y < startPosition.y + maxDistance && targetPosition.y > startPosition.y;

    }

    private void DelegateFuntions()
    {

        switch (movingAxes)
        {
            case MovingAxes.X:
                if (EndPosition.position.x > startPosition.x)
                {
                    endPosIsLeftOfStart = true;
                }
                getTargetPosition = GetTargetPositionX;
                canMove = CanMoveX;
                break;
            case MovingAxes.Z:
                if (EndPosition.position.z > startPosition.z)
                {
                    endPosIsLeftOfStart = true;
                }
                getTargetPosition = GetTargetPositionZ;
                canMove = CanMoveZ;
                break;
            case MovingAxes.Y:
                getTargetPosition = GetTargetPositionY;
                canMove = CanMoveY;
                break;

        }

        
    }


}


