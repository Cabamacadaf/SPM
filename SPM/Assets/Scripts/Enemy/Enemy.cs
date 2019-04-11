using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : StateMachine
{
    [HideInInspector] public MeshRenderer meshRenderer;
    [HideInInspector] public NavMeshAgent agent;

    [HideInInspector] public Player player;

    protected override void Awake ()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
        base.Awake();
    }

    public void Damage (float speed, float damage)
    {
        Debug.Log("Damage doesn't work, please fix me");
    }
}
