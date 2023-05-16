using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;

    private void Awake()
    {
        healthSlider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int value, bool resetHealth)
    {
        healthSlider.maxValue = value;
        if (resetHealth) healthSlider.value = value;
    }
    public void SetHealth(int value)
    {
        healthSlider.value = value;
    }

    public void TakeDamage(int value)
    {
        healthSlider.value -= value;
    }

    public void GetHealed(int value)
    {
        healthSlider.value += value;
    }
}
