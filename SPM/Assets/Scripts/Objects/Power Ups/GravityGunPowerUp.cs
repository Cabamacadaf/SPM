using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGunPowerUp : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            UpgradeSettings.instance.HasUpgrade = true;

            ObjectDestroyedEvent destroyEvent = new ObjectDestroyedEvent(this.gameObject);
            destroyEvent.ExecuteEvent();
        }
    }
}
