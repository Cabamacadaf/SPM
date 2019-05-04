using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenWoodTrigger : MonoBehaviour
{

    public GameObject wood1;
    public GameObject wood2;
    public GameObject wood3, wood4, wood5, wood6;
    public GameObject SpawnTrigger;
    // Start is called before the first frame update
    void Start()
    {
        wood1.GetComponent<Rigidbody>().isKinematic = true;
        wood2.GetComponent<Rigidbody>().isKinematic = true;
        wood3.GetComponent<Rigidbody>().isKinematic = true;
        wood4.GetComponent<Rigidbody>().isKinematic = true;
        wood5.GetComponent<Rigidbody>().isKinematic = true;
        wood6.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnTrigger.SetActive(false);
            Debug.Log("Touched");
            wood1.GetComponent<Rigidbody>().isKinematic = false;
            wood2.GetComponent<Rigidbody>().isKinematic = false;
            wood3.GetComponent<Rigidbody>().isKinematic = false;
            wood4.GetComponent<Rigidbody>().isKinematic = false;
            wood5.GetComponent<Rigidbody>().isKinematic = false;
            wood6.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

}
