using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Author: Simon Sundström
 * 
 */

[System.Serializable]
public class PickUpData
{
    public float[] position;
    public float[] rotation;

    public PickUpData(PickUpObject pickUpObject)
    {
        position = new float[3];
        position[0] = pickUpObject.transform.position.x;
        position[1] = pickUpObject.transform.position.y;
        position[2] = pickUpObject.transform.position.z;

        rotation = new float[3];
        rotation[0] = pickUpObject.transform.rotation.x;
        rotation[1] = pickUpObject.transform.rotation.y;
        rotation[2] = pickUpObject.transform.rotation.z;
    }

}
