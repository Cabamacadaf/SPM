using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private Light[] lights;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
