using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //public Slider healthSlider;
    public Image healthFill;

    private int maxHealth;

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
    public void SetHealth(int value)
    {
        //healthSlider.value = value;
        healthFill.fillAmount = (float)value / maxHealth;
    }

    public void TakeDamage(int value)
    {
        //healthSlider.value -= value;
        healthFill.fillAmount -= (float)value / maxHealth;
    }

    public void GetHealed(int value)
    {
        //healthSlider.value += value;
        healthFill.fillAmount += (float)value / maxHealth;
    }
}
