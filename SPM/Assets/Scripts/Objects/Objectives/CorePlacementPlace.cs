//Main Author: Marcus Mellström
//Secondary Author: John Tullander

using System.Collections;
using UnityEngine;

public class CorePlacementPlace : MonoBehaviour
{
    [SerializeField] private Light[] lights;
    [SerializeField] private Color newColor;
    [SerializeField] private float timeBetweenLights;
    [SerializeField] private float timeUntillLightsActivate;
    public GameObject hit;

    [SerializeField] private GameObject energiLightsSmall;
    [SerializeField] private GameObject energiLightsBig;
    private GravityGun gravityGun;

    private bool active = true;
    bool finish = false;

    private void Awake ()
    {
        gravityGun = FindObjectOfType<GravityGun>();
    }

    private void OnTriggerEnter (Collider other)
    {
        if (active && other.CompareTag("PowerCore")) {
            StartCoroutine(SetActiveOBJ());
            active = false;

            if (gravityGun.GetCurrentState() is HoldingState) {
                HoldingState holdingState = (HoldingState)gravityGun.GetCurrentState();
                holdingState.Drop();
            }


            other.transform.position = hit.transform.position;
            other.transform.parent = hit.transform;
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
        foreach (Light light in lights) {
            light.color = newColor;
            yield return new WaitForSeconds(timeBetweenLights);
        }
    }

    private IEnumerator SetActiveOBJ ()
    {
        yield return new WaitForSeconds(timeUntillLightsActivate);
        energiLightsBig.SetActive(true);
        energiLightsSmall.SetActive(true);
    }
}
