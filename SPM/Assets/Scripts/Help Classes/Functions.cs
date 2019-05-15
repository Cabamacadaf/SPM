using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions : MonoBehaviour
{
    public static Vector3 CalculateNormalForce(Vector3 velocity, Vector3 normal)
    {
        float dotProduct = Vector3.Dot(velocity, normal);

        if (dotProduct > 0)
        {
            dotProduct = 0;
        }

        Vector3 projection = dotProduct * normal;

        return -projection;
    }

    public static Vector2 CalculateNormalForce2D(Vector2 velocity, Vector2 normal)
    {
        float dotProduct = Vector2.Dot(velocity, normal);

        if (dotProduct > 0)
        {
            dotProduct = 0;
        }

        Vector2 projection = dotProduct * normal;

        return -projection;
    }

    public static float CalculateFriction(float normalForce, float friction)
    {
        return normalForce * friction;
    }

    public static bool IsInLayerMask (int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}
