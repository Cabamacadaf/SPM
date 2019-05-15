using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCoreObjectiveController : MonoBehaviour
{
    [SerializeField] private GameObject hit;

    private GravityGun gravityGun;
    private bool active = true;
    private bool finish = false;

    private void Awake()
    {
        gravityGun = FindObjectOfType<GravityGun>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (active && other.CompareTag("PowerCore"))
        {

            active = false;

            if (gravityGun.GetCurrentState() is GravityGunBaseState)
            {
                GravityGunBaseState holdingState = (GravityGunBaseState)gravityGun.GetCurrentState();
                holdingState.DropObject();
            }

            TransformObject(other);

            PowerCorePlacedEvent objectiveEvent = new PowerCorePlacedEvent();
            objectiveEvent.eventDescription = "Power Core placed.";
            objectiveEvent.ExecuteEvent();

        }
    }

    private void TransformObject(Collider other)
    {
        other.transform.position = hit.transform.position;
        other.transform.parent = hit.transform;
        other.transform.rotation = Quaternion.identity;
        other.gameObject.layer = 0;
        Destroy(other.GetComponent<Rigidbody>());
    }
}
