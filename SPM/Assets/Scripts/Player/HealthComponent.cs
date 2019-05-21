using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float startHealth;

    public float Health { get; set; }
    // Start is called before the first frame update
    void Start()
    { 
        Health = startHealth;
    }

    public void Damage(float amount)
    {
        Health -= amount;
        Debug.Log("Health: " + Health);

        if(Health <= 0)
        {
            DeathEvent deathEvent = new DeathEvent(this.gameObject);
            deathEvent.ExecuteEvent();
            Health = 100;

        }
    }

    public void Addhealth(float healthToAdd)
    {
        Health += healthToAdd;
        if (Health > 100.0f)
        {
            Health = 100.0f;
        }
    }
}
