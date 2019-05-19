//Author: Simon Sundström
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPuzzleTrigger : MonoBehaviour
{
    private int id;
    private bool isActive = true;

    [SerializeField] private float throwForce = 100.0f;
    [SerializeField] private float rotationSpeed = 10.0f;

    private Vector3 rotationDirection;

    private Transform powerCubeOriginalParent;
    private Transform attachedPowerCube;
    private Rigidbody attachedPowerCubeRigidBody;

    [SerializeField] private GameObject lights;

    private GravityGun gravityGun;

    private void Awake ()
    {
        FinalPuzzleFailEvent.RegisterListener(ObjectiveFailed);
        gravityGun = FindObjectOfType<GravityGun>();
        id = GetComponent<ID>().NR;
    }

    private void Update ()
    {
        if(attachedPowerCube != null){
            attachedPowerCube.Rotate(rotationDirection * rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if (isActive && other.CompareTag("PuzzleObject") && other.GetComponent<ID>().NR == id) {
            isActive = false;

            if (gravityGun.HoldingObject != null) {
                GravityGunBaseState gravityGunState = (GravityGunBaseState)gravityGun.GetCurrentState();
                gravityGunState.DropObject(false);
            }

            AttachObject(other);

            FinalPuzzleObjectiveController.Instance.TryToAddPowerCube(id);
        }
    }

    private void AttachObject (Collider other)
    {
        rotationDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;

        attachedPowerCubeRigidBody = other.attachedRigidbody;
        attachedPowerCubeRigidBody.isKinematic = true;

        attachedPowerCube = other.transform;
        attachedPowerCube.position = transform.position;
        powerCubeOriginalParent = attachedPowerCube.parent;
        attachedPowerCube.parent = transform;
        attachedPowerCube.gameObject.layer = 0;

        lights.SetActive(true);
    }

    private void ObjectiveFailed (FinalPuzzleFailEvent finalPuzzleFailEvent)
    {
        if (attachedPowerCube != null) {
            isActive = true;
            attachedPowerCube.SetParent(powerCubeOriginalParent);
            attachedPowerCube.gameObject.layer = LayerMask.NameToLayer("Pick Up Objects");
            attachedPowerCube = null;

            attachedPowerCubeRigidBody.isKinematic = false;
            attachedPowerCubeRigidBody.AddForce(new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)).normalized * throwForce);
            attachedPowerCubeRigidBody = null;

            lights.SetActive(false);
        }
    }
}
