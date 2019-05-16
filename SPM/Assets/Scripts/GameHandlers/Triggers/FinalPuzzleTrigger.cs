//Author: Simon Sundström
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FinalPuzzleTrigger : MonoBehaviour
{
    private bool isActive = true;


    private GravityGun gravityGun;

    private void Awake()
    {
        gravityGun = FindObjectOfType<GravityGun>();
    }

    private void OnTriggerEnter(Collider other)
    {
     
        if (isActive && other.CompareTag("PuzzleObject") && other.GetComponent<ID>().NR == GetComponent<ID>().NR)
        {
            Debug.Log("Collided");

            isActive = false;
            if (gravityGun.holdingObject != null)
            {
                GravityGunBaseState gravityGunState = (GravityGunBaseState)gravityGun.GetCurrentState();
                gravityGunState.DropObject();
            }

            TransformObject(other);

            GameController.Instance.AddLastPuzzle();

        }
    }

    //private void OnTriggerExit (Collider other)
    //{
    //    if (other.CompareTag("PuzzleObject") && other.GetComponent<ID>().NR == GetComponent<ID>().NR) {

    //        GameController.Instance.AddLastPuzzle();

    //    }
    //}

    private void TransformObject(Collider other)
    {
  
        Destroy(other.GetComponent<Rigidbody>());
        other.transform.position = transform.position;
        other.transform.parent = transform;
        other.transform.rotation = Quaternion.identity;
        other.gameObject.layer = 0;

    }
}
