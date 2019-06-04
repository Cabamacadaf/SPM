
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectsData
{
    public List<PickUpData> activeObjects = new List<PickUpData>();
    public List<EnemyData> activeEnemies = new List<EnemyData>();
    public List<DestructibleObjectData> activeDestructibleObjects = new List<DestructibleObjectData>();
    public List<HealthPackData> activeHealthPacks = new List<HealthPackData>();
}
