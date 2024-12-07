using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MazotBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxMazot(float _value)
    {
        slider.maxValue = _value;
        SetCurrentMazot(_value);
    }

    public void SetCurrentMazot(float _currentMazot)
    {
        slider.value = _currentMazot;
    }
}
