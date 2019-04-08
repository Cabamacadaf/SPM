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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ApplyGravity();
        ApplyAirResistance();



    }

    public void AddVelocity(Vector3 vel)
    {
        velocity += vel;
    }

    public void DecreaseVelocity(Vector3 vel)
    {
        velocity -= vel;
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }

    public void SetVelocity(Vector2 vel)
    {
        velocity = vel;
    }

    private void ApplyGravity()
    {
        velocity += Vector3.down * gravity * Time.deltaTime;

    }
    private void ApplyAirResistance()
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

    public float CalculateStaticFriction(float normalForceMagnitude)
    {
        return Functions.CalculateFriction(normalForceMagnitude, staticFrictionCoefficient);
    }

    public float CalculateDynamicFriction(float normalForceMagnitude)
    {
        return Functions.CalculateFriction(normalForceMagnitude, dynamicFrictionCoefficient);
    }

    public void HandlePlatformCollision(float normalForceMagnitude, GameObject hit)
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
