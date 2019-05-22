﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float startHealth;
    private Slider slider;
    public float Health { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        slider.value = startHealth;
        Health = startHealth;
    }

    public void Damage(float amount)
    {
        Health -= amount;
        slider.value -= amount;
        if(Health <= 0)
        {
            DeathEvent deathEvent = new DeathEvent(this.gameObject);
            deathEvent.ExecuteEvent();
            Health = 100;
            slider.value = Health;

        }
    }

    public void Addhealth(float healthToAdd)
    {
        Health += healthToAdd;
        if (Health > 100.0f)
        {
            Health = 100.0f;
            slider.value = Health;
        }
    }
}
