using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private bool active = false;
    private Transform player;
    private float pullForce;
    private Transform pullPoint;

    void Awake()
    {
        player = FindObjectOfType<PlayerController3D>().transform;
    }
    
    void Update()
    {
        if(active == true) {
            transform.position += (pullPoint.position - transform.position).normalized * pullForce * Time.deltaTime;
        }
    }

    public void Pull (float pullForce, Transform pullPoint)
    {
        this.pullPoint = pullPoint;
        this.pullForce = pullForce;
        active = true;
    }
}
