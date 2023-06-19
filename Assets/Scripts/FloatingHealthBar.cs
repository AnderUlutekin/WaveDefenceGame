using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    public void UpdateHealthBar(float currentVal, float maxVal)
    {
        slider.value = currentVal / maxVal;
    }
}
