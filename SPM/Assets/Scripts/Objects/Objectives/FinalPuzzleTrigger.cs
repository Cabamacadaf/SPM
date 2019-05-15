//Author: Simon Sundström
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FinalPuzzleTrigger : MonoBehaviour
{
    [SerializeField] private Light[] lights;

    private void Start ()
    {
        setLightFalse();
    }

    private void OnTriggerEnter(Collider other)
    {
     
        if (other.CompareTag("PuzzleObject") && other.GetComponent<ID>().NR == GetComponent<ID>().NR)
        {

            TransformObject(other);

            //Bestäm vad ljusen ska göra!!
            HandleLight();

            Debug.Log("?????");
            GameController.Instance.AddLastPuzzle();

        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("PuzzleObject") && other.GetComponent<ID>().NR == GetComponent<ID>().NR) {


            foreach (Light light in lights) {
                light.enabled = false;
            }
            GameController.Instance.AddLastPuzzle();

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
            light.enabled = true;
        }
    }

    private void setLightFalse ()
    {
        foreach (Light light in lights) {
            Debug.Log("SetFalse");
            light.enabled = false;
        }
    }
}
