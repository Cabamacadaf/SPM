using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightComponent : MonoBehaviour
{
    [SerializeField] private Light[] lights;

    //Från CorePlacement
    [SerializeField] private Color newColor;
    [SerializeField] private float timeBetweenLights;
    [SerializeField] private float timeUntillLightsActivate;
    //[SerializeField] private GameObject energiLightsSmall;
    //[SerializeField] private GameObject energiLightsBig;



    public void EnableLights()
    {
        foreach (Light light in lights)
        {
            light.enabled = true;
        }
    }

    public void DisableLights()
    {
        foreach (Light light in lights)
        {
            light.enabled = false;
        }
    }

    public IEnumerator ChangeColors()
    {
        foreach (Light light in lights)
        {
            light.color = newColor;
            yield return new WaitForSeconds(timeBetweenLights);
        }
    }

    //public IEnumerator SetActiveOBJ()
    //{
    //    yield return new WaitForSeconds(timeUntillLightsActivate);
    //    energiLightsBig.SetActive(true);
    //    energiLightsSmall.SetActive(true);
    //}
}
