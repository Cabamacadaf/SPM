using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCoreObjectiveController : MonoBehaviour
{
    [SerializeField] private Transform corePosition;

    private GravityGun gravityGun;
    private bool isActive = true;

    private void Awake()
    {
        gravityGun = FindObjectOfType<GravityGun>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive && other.CompareTag("PowerCore"))
        {

            isActive = false;

            if (gravityGun.HoldingObject != null)
            {
                GravityGunBaseState gravityGunState = (GravityGunBaseState)gravityGun.GetCurrentState();
                gravityGunState.DropObject(false);
            }

            TransformObject(other);

            PowerCorePlacedEvent objectiveEvent = new PowerCorePlacedEvent();
            objectiveEvent.EventDescription = "Power Core placed.";
            objectiveEvent.ExecuteEvent();

        }
    }

    private void TransformObject(Collider other)
    {
        Destroy(other.GetComponent<Rigidbody>());
        other.transform.position = corePosition.transform.position;
        other.transform.parent = corePosition.transform;
        other.transform.rotation = Quaternion.identity;
        other.gameObject.layer = 0;
    }
}
