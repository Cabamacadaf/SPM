using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent : MonoBehaviour
{
    //Movement
    private Vector3 velocity;
    [SerializeField] private float airResistanceCoefficient;

    [SerializeField] private float staticFrictionCoefficient;
    [SerializeField] private float dynamicFrictionCoefficient;

    //Gravity
    [SerializeField] private float gravity;

    protected void AddVelocity(Vector3 vel)
    {
        velocity += vel;
    }

    protected void DecreaseVelocity(Vector3 vel)
    {
        velocity -= vel;
    }

    protected Vector3 GetVelocity()
    {
        return velocity;
    }

    protected void SetVelocity(Vector2 vel)
    {
        velocity = vel;
    }

    protected void ApplyGravity()
    {
        velocity += Vector3.down * gravity * Time.deltaTime;

    }
    protected void ApplyAirResistance()
    {
        velocity *= Mathf.Pow(airResistanceCoefficient, Time.deltaTime);

    }

    protected void ApplyFriction(float normalForceMagnitude)
    {

        if (velocity.magnitude < CalculateStaticFriction(normalForceMagnitude))
        {


            velocity = Vector3.zero;
        }
        else
        {

            velocity += -velocity.normalized * CalculateDynamicFriction(normalForceMagnitude);

        }
    }

    protected float CalculateStaticFriction(float normalForceMagnitude)
    {
        return Functions.CalculateFriction(normalForceMagnitude, staticFrictionCoefficient);
    }

    protected float CalculateDynamicFriction(float normalForceMagnitude)
    {
        return Functions.CalculateFriction(normalForceMagnitude, dynamicFrictionCoefficient);
    }

    protected void HandlePlatformCollision(float normalForceMagnitude, GameObject hit)
    {
        float difference = velocity.x - hit.transform.GetComponent<PhysicsComponent>().GetVelocity().x;

        if (difference < CalculateStaticFriction(normalForceMagnitude))
        {

            velocity.x = difference - velocity.x;
        }
        else
        {
            velocity += -velocity.normalized * CalculateDynamicFriction(normalForceMagnitude);
        }
    }

}
