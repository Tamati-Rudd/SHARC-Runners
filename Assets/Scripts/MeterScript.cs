using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterScript : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxAbility(float ability)
    {
        slider.maxValue = ability;
        slider.value = ability;

        fill.color = gradient.Evaluate (1f) ;

    }

    public void SetAbility(float ability)
    {
        slider.value = ability;
        fill.color = gradient.Evaluate(slider.normalizedValue);

    }
}
