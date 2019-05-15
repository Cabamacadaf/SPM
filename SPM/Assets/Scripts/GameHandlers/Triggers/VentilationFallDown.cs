using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentilationFallDown : MonoBehaviour
{

    private GameObject thisGameObjekt;
    // Start is called before the first frame update
    void Start()
    {
        thisGameObjekt = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player")) {
            thisGameObjekt.SetActive(false);
        }
    }
}
