using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaComponent : MonoBehaviour
{

    public float MaxStamina { get; set; }
    public float Stamina { get; set; }

    [SerializeField] private float AddStaminaRate = 15f;
    [SerializeField] private float LoseStaminaRate = 15f;

    private void Awake()
    {
        MaxStamina = 100;
        Stamina = MaxStamina;
    }

    public void RecoverStamina()
    {
        Stamina += AddStaminaRate * Time.deltaTime;
        if (Stamina >= MaxStamina)
        {
            Stamina = MaxStamina;
        }

    }

    public void UseStamina()
    {
        Stamina -= LoseStaminaRate * Time.deltaTime;
        if (Stamina <= 0)
        {
            Stamina = 0;
        }
    }
}
