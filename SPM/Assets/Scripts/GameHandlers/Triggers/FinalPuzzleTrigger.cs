//Author: Simon Sundström
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FinalPuzzleTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
     
        if (other.CompareTag("PuzzleObject") && other.GetComponent<ID>().NR == GetComponent<ID>().NR)
        {

            TransformObject(other);

            GameController.Instance.AddLastPuzzle();

        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("PuzzleObject") && other.GetComponent<ID>().NR == GetComponent<ID>().NR) {

            GameController.Instance.AddLastPuzzle();

        }
    }

    private void TransformObject(Collider other)
    {
        other.transform.position = transform.position;
        other.transform.parent = transform;
        other.transform.rotation = Quaternion.identity;
    
    }
}
