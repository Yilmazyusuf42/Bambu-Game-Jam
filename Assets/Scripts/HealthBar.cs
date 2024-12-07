using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(float _value)
    {
        slider.maxValue = _value;
        SetCurrentHealth(_value);
    }

    public void SetCurrentHealth(float _currentHealth)
    {
        slider.value = _currentHealth;
    }
}
