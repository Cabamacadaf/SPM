using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private bool active = false;
    private Transform player;
    private float pullForce;

    void Awake()
    {
        player = FindObjectOfType<PlayerController3D>().transform;
    }
    
    void Update()
    {
        if(active == true) {
            transform.position += (player.position - transform.position).normalized * pullForce * Time.deltaTime;
        }
    }

    public void Pull (float pullForce)
    {
        this.pullForce = pullForce;
        active = true;
    }
}
