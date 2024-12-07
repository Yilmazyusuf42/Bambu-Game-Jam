using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class YururBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxYurur(float _value)
    {
        slider.maxValue = _value;
        SetCurrentYurur(_value);
    }

    public void SetCurrentYurur(float _currentYurur)
    {
        slider.value = _currentYurur;
    }
}
