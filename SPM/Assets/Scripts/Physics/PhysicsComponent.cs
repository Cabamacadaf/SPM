using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent : MonoBehaviour
{
    //Attributes
    protected Vector3 velocity;
    [SerializeField] private float airResistanceCoefficient;
    [SerializeField] private float staticFrictionCoefficient;
    [SerializeField] protected float dynamicFrictionCoefficient;
    public float gravity;

    //Methods

    public void AddVelocity(Vector3 vel)
    {
        velocity += vel;
    }

    public void Decelerate(Vector3 vel)
    {
        velocity -= vel;
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }

    public void SetVelocity(Vector3 vel)
    {
        velocity = vel;
    }

    public void ApplyGravity()
    {
        velocity += Vector3.down * gravity * Time.deltaTime;

    }
    public void ApplyAirResistance()
    {
        velocity *= Mathf.Pow(airResistanceCoefficient, Time.deltaTime);

    }

    public void ApplyFriction(float normalForceMagnitude)
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


}
