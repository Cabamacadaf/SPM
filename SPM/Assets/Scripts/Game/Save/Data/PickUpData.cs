using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Author: Simon Sundström
 * 
 */

[Serializable]
public class PickUpData
{
    public int ID;
    public float[] position;
    public float[] rotation;

    public PickUpData(GameObject pickUpObject)
    {
        ID = pickUpObject.GetComponent<PickUpObject>().ID;

        position = new float[3];
        position[0] = pickUpObject.transform.position.x;
        position[1] = pickUpObject.transform.position.y;
        position[2] = pickUpObject.transform.position.z;

        rotation = new float[3];
        rotation[0] = pickUpObject.transform.eulerAngles.x;
        rotation[1] = pickUpObject.transform.eulerAngles.y;
        rotation[2] = pickUpObject.transform.eulerAngles.z;
    }

    public override string ToString ()
    {
        return ID + " " + position[0] + position[1] + position[2];
    }
}
