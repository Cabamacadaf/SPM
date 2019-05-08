﻿//Author: Simon Sundström
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FinalPuzzleTrigger : MonoBehaviour
{
    [SerializeField] private List<Light> lights;

    

    private void OnTriggerEnter(Collider other)
    {
     
        if (other.CompareTag("PuzzleObject") && other.GetComponent<ID>().NR == GetComponent<ID>().NR)
        {

            TransformObject(other);

            //Bestäm vad ljusen ska göra!!
            HandleLight();

            Debug.Log("?????");
            GameController_2.gameControllerInstance_2.greenCollecting++;

        }
    }

    private void TransformObject(Collider other)
    {
        other.transform.position = transform.position;
        other.transform.parent = transform;
        other.transform.rotation = Quaternion.identity;
    
    }

    private void HandleLight()
    {
        foreach(Light light in lights)
        {
            //Gör det du vill göra med lights här
        }
    }
}