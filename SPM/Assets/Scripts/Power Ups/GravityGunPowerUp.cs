using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGunPowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");
        if (other.CompareTag("Player"))
        {
            GravityGun gravityGun = other.GetComponentInChildren<GravityGun>();
            gravityGun.PowerUp();
            Destroy(this.gameObject);
        }
    }
}
