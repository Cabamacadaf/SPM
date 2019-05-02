using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorePlacementPlace : MonoBehaviour
{
    [SerializeField] private Light[] lights;
    [SerializeField] private Color newColor;
    [SerializeField] private float timeBetweenLights;

    private bool active = true;

    private void OnTriggerEnter (Collider other)
    {
        if (active && other.CompareTag("PowerCore"))
        {
            active = false;
            other.transform.position = transform.position;
            other.transform.parent = transform;
            other.transform.rotation = Quaternion.identity;
            other.gameObject.layer = 0;
            Destroy(other.GetComponent<Rigidbody>());

            PowerCorePlacedEvent objectiveEvent = new PowerCorePlacedEvent();
            objectiveEvent.eventDescription = "Power Core placed.";
            objectiveEvent.ExecuteEvent();

            StartCoroutine(ChangeColors());
        }
    }
    private IEnumerator ChangeColors ()
    {
        foreach (Light light in lights){
            light.color = newColor;
            yield return new WaitForSeconds(timeBetweenLights);
        }
    }
}
