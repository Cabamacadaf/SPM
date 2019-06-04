//Main Author: Simon Sundström
//Secondary Author: Marcus Mellström

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float startHealth;
    [SerializeField] private Slider healthSlider;
    public float Health { get; set; }

    void Awake()
    {
        Health = startHealth;
        UpdateHealthSlider();
    }

    public void Damage(float amount)
    {
        Health -= amount;
        Debug.Log(Health);
        if(Health <= 0)
        {
            DeathEvent deathEvent = new DeathEvent(gameObject);
            deathEvent.ExecuteEvent();
        }
        UpdateHealthSlider();
    }

    public void Addhealth(float healthToAdd)
    {
        Health += healthToAdd;
        if (Health > 100.0f)
        {
            Health = 100.0f;
        }
        UpdateHealthSlider();
    }

    private void UpdateHealthSlider()
    {
        healthSlider.value = Health;
    }
}
