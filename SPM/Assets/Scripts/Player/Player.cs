using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine
{


    //Attributes

    public float groundCheckDistance;
    public GameObject mainCamera;
    public LayerMask layerMask;
    public float skinWidth;
    [HideInInspector] public CapsuleCollider capsuleCollider;
    [HideInInspector] public PhysicsComponent physics;
    public GravityGun gravityGun;

    protected override void Awake()
    {
        physics = GetComponent<PhysicsComponent>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        base.Awake();

    }

}
