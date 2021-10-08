using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public float changeSpeed=50f;
    public int Health;


    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        if (fill != null)
        {
            fill.color = gradient.Evaluate(1f);

        }
        Health = (int)slider.maxValue;
    }

    public void SetHealth(int health)
    {
        //slider.value = health;
        Health = health;

        if (fill != null)
        {
            fill.color = gradient.Evaluate(slider.normalizedValue);

        }

    }

    public void FixedUpdate()
    {
        slider.value = Mathf.MoveTowards(slider.value, Health, changeSpeed * Time.deltaTime);
    }
}