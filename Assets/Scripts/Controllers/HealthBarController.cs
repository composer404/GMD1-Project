using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private Slider slider;

    void Start() {
        slider = gameObject.GetComponent<Slider>();
    }

    public void SetMaxHealth(int health) {
        slider.maxValue = health;
    }

    public void SetHealth(int health) {
        slider.value = health;
    }
}
