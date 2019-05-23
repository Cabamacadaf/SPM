using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaComponent : MonoBehaviour
{

    public float MaxStamina { get; set; }
    public float Stamina { get; set; }

    [SerializeField] private Slider staminaSlider;

    [SerializeField] private float addStaminaRate = 15f;
    [SerializeField] private float loseStaminaRate = 15f;

    private void Awake()
    {
        MaxStamina = 100;
        Stamina = MaxStamina;
    }

    public void RecoverStamina()
    {
        Stamina += addStaminaRate * Time.deltaTime;
        if (Stamina >= MaxStamina)
        {
            Stamina = MaxStamina;
        }
        staminaSlider.value = Stamina;
    }

    public void UseStamina()
    {
        Stamina -= loseStaminaRate * Time.deltaTime;
        if (Stamina <= 0)
        {
            Stamina = 0;
        }
        staminaSlider.value = Stamina;
    }
}
