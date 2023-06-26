using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //public Slider healthSlider;
    public Image healthFill;

    [SerializeField] private int maxHealth = 5;

    private void Awake()
    {
        //healthSlider = GetComponent<Slider>();
        healthFill = GetComponentInChildren<Image>();
    }

    public void SetMaxHealth(int value, bool resetHealth)
    {
        //healthSlider.maxValue = value;
        //if (resetHealth) healthSlider.value = value;
        maxHealth = value;
        healthFill.fillAmount = 1f;
    }

    public void ResetHealth()
    {
        healthFill.fillAmount = 1f;
    }
    public void SetHealth(float value, bool reverse = false)
    {
        //healthSlider.value = value;
        if (reverse)
        {
            healthFill.fillAmount = 1 - ( value / maxHealth);
        }
        else
        {
            healthFill.fillAmount = value / maxHealth;
        }
    }

    public void TakeDamage(float value)
    {
        //healthSlider.value -= value;
        healthFill.fillAmount -= value / maxHealth;
    }

    public void GetHealed(float value)
    {
        //healthSlider.value += value;
        healthFill.fillAmount += (float)value / maxHealth;
    }
}
