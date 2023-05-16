using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image fill;
    private float maxHealth;

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
    }

    public void ChangeHealth(float value)
    {
        value /= maxHealth;
        fill.fillAmount += value;
    }
}
