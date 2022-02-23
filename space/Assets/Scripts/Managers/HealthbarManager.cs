using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarManager : MonoBehaviour
{
    private Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = InfoManager.instance._sataliteAmount;
        slider.value = 0;
    }

    private void Update()
    {
        slider.value += InfoManager.instance._connectedSatalites;
    }
}
