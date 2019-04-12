using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : StateMachine
{
    [HideInInspector] public MeshRenderer meshRenderer;

    [HideInInspector] public Player player;

    protected override void Awake ()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        player = FindObjectOfType<Player>();
        base.Awake();
    }
}
