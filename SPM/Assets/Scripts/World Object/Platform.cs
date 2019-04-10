using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : PhysicsComponent
{
    [SerializeField] private float movementSpeed;


    [SerializeField] private Vector3 direction;
    private Transform startPosition;
    private float counter;


    // Start is called before the first frame update
    private void Awake()
    {
    }
    void Start()
    {
        startPosition = this.transform;
        counter = 0;

    }
    //Riktning * movespeed * tid
    // Update is called once per frame
    void Update()
    {
        ApplyAirResistance();
        if (counter > 6)
        {
            direction = -direction;
            counter = 0;
        }
            

        AddVelocity(direction * movementSpeed * Time.deltaTime);
        //vel = direction * movementSpeed * Time.deltaTime * Time.deltaTime;
        transform.position += GetVelocity() * Time.deltaTime;
        counter+=Time.deltaTime;
    }

}
