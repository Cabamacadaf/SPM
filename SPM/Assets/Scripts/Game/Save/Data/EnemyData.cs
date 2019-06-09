//Author: Simon Sundström
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public int aggro;
    public int dead;
    public int ID;

    public float[] position;
    public float[] rotation;

    public EnemyData(Enemy enemy)
    {
        ID = enemy.ID;

        if(enemy.GetCurrentState() is EnemyIdleState) {
            aggro = 0;
        }
        else {
            aggro = 1;
        }

        if(enemy.gameObject.activeInHierarchy == true) {
            dead = 0;
        }
        else {
            dead = 1;
        }

        position = new float[3];
        position[0] = enemy.transform.position.x;
        position[1] = enemy.transform.position.y;
        position[2] = enemy.transform.position.z;

        rotation = new float[3];
        rotation[0] = enemy.transform.eulerAngles.x;
        rotation[1] = enemy.transform.eulerAngles.y;
        rotation[2] = enemy.transform.eulerAngles.z;
    }
}
