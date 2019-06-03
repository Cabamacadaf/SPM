using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DestructibleObjectData
{
    public int ID;
    public int destroyed;


    public DestructibleObjectData (DestructibleObject destructibleObject)
    {
        ID = destructibleObject.ID;
        if(destructibleObject.enabled == true) {
            destroyed = 1;
        }
        else {
            destroyed = 0;
        }
    }
}
