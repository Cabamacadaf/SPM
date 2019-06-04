using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthPackData
{
    public int ID;
    public int used;

    public HealthPackData(HealthPack healthPack)
    {
        ID = healthPack.ID;
        if (healthPack.enabled)
        {
            used = 1;
        }
        else
        {
            used = 0;
        }
    }
}
