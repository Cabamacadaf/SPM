using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public float[] position;
    public float[] rotation;

    public EnemyData(Enemy enemy)
    {
        position = new float[3];
        position[0] = enemy.transform.position.x;
        position[1] = enemy.transform.position.y;
        position[2] = enemy.transform.position.z;

        rotation = new float[3];
        rotation[0] = enemy.transform.rotation.x;
        rotation[1] = enemy.transform.rotation.y;
        rotation[2] = enemy.transform.rotation.z;
    }
}
