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
        slider.maxValue = ability;//initiate a max value for slider
        slider.value = ability;//initiate a max slider value

        fill.color = gradient.Evaluate (1f) ;//when the bar increases the colour gets brighter

    }

    public void SetAbility(float ability)
    {
        slider.value = ability;//change the slider value
        fill.color = gradient.Evaluate(slider.normalizedValue);//change the colour of the meter

    }
}
