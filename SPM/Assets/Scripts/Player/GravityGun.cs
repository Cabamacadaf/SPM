using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityGun : MonoBehaviour
{
    [SerializeField] private float pushRange;
    [SerializeField] private float pullRange;
    [SerializeField] private float pushForce;
    [SerializeField] private float pullForce;
    [SerializeField] private float cameraOffset;

    [SerializeField] private LayerMask hitLayer;

    [SerializeField] private Image crosshair;

    private PickUpObject holdingObject;

    [SerializeField] private float playerPushForce;

    //Power up
    [SerializeField] private float powerUpLength;
    [SerializeField] private float powerUpMultiplier;


    public void Update()
    {
        if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward * cameraOffset, Camera.main.transform.forward, out RaycastHit hit, pushRange, hitLayer) && hit.transform.GetComponent<PickUpObject>() != null)
        {
            crosshair.color = Color.green;
        }
        else if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward * cameraOffset, Camera.main.transform.forward, out RaycastHit hitInfo, pullRange, hitLayer) && hit.transform.GetComponent<PickUpObject>() != null)
        {
            crosshair.color = Color.green;
        }
        else
        {
            crosshair.color = Color.red;
        }
    }

    public void Push()
    {
        if (holdingObject == null)
        {
            if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward * cameraOffset, Camera.main.transform.forward, out RaycastHit hit, pushRange, hitLayer) && hit.transform.GetComponent<PickUpObject>() != null)
            {
                Debug.DrawLine(Camera.main.transform.position + Camera.main.transform.forward * cameraOffset, hit.point, Color.red, 2);
                if (hit.collider.attachedRigidbody != null && hit.collider.GetComponent<PickUpObject>() != null)
                {
                    hit.collider.attachedRigidbody.AddForce(Camera.main.transform.forward * pushForce * (1 - (hit.distance / pushRange)));
                }
                else if (hit.collider.gameObject.CompareTag("Platform"))
                {
                    //GetComponentInParent<PlayerController3D>().AddVelocity(hit.normal * playerPushForce);
                }
            }
            else
            {
                Debug.DrawRay(Camera.main.transform.position + Camera.main.transform.forward * cameraOffset, Camera.main.transform.forward * pushRange, Color.blue, 2);
            }
        }

        else if (holdingObject.holding)
        {
            holdingObject.Drop();
            holdingObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * pushForce);
            holdingObject = null;
        }
    }

    public void Pull()
    {
        if (holdingObject == null)
        {
            if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward * cameraOffset, Camera.main.transform.forward, out RaycastHit hit, pullRange, hitLayer) && hit.transform.GetComponent<PickUpObject>() != null)
            {
                Debug.DrawLine(Camera.main.transform.position + Camera.main.transform.forward * cameraOffset, hit.point, Color.green, 2);
                holdingObject = hit.collider.GetComponent<PickUpObject>();
                holdingObject.Pull(pullForce);
            }
            else
            {
                Debug.DrawRay(Camera.main.transform.position + Camera.main.transform.forward * cameraOffset, Camera.main.transform.forward * pushRange, Color.blue, 2);
            }
        }

        else
        {
            holdingObject.Drop();
            holdingObject = null;
        }
    }

    private IEnumerator PowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);

        pushRange -= 10;
        pullRange -= 10;
    }

    public void PowerUp()
    {
        pushRange += 10;
        pullRange += 10;
        StartCoroutine(PowerDownRoutine());
    }
}
