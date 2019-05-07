﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorePlacementPlace : MonoBehaviour
{
    [SerializeField] private Light[] lights;
    [SerializeField] private Color newColor;
    [SerializeField] private float timeBetweenLights;
    [SerializeField] private float timeUntillLightsActivate;

    [SerializeField] private GameObject energiLightsSmall;
    [SerializeField] private GameObject energiLightsBig;

    private bool active = true;
    bool finish = false;

    private void Update()
    {
    }

    private void OnTriggerEnter (Collider other)
    {
        if (active && other.CompareTag("PowerCore"))
        {
            if (active)
            {
                StartCoroutine(SetActiveOBJ());
            }
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

    private IEnumerator SetActiveOBJ()
    {
        yield return new WaitForSeconds(timeUntillLightsActivate);
        energiLightsBig.SetActive(true);
        energiLightsSmall.SetActive(true);
    }
}
