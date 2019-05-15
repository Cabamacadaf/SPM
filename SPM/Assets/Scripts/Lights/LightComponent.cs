using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightComponent : MonoBehaviour
{
    [SerializeField] private Light[] lights;


    [SerializeField] private Color newColor;
    [SerializeField] private float timeBetweenLights;
    [SerializeField] private float timeUntillLightsActivate;
    [SerializeField] private GameObject energiLightsSmall;
    [SerializeField] private GameObject energiLightsBig;


    private void EnableLights()
    {
        foreach (Light light in lights)
        {
            light.enabled = true;
        }
    }

    private void DisableLights()
    {
        foreach (Light light in lights)
        {
            light.enabled = false;
        }
    }

    private IEnumerator ChangeColors()
    {
        foreach (Light light in lights)
        {
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
