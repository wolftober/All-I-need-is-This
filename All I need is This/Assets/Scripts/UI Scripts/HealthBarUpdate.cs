using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUpdate : MonoBehaviour
{
    public PlayerData playerData;
    public Slider slider;

    private void Start()
    {
        slider.maxValue = playerData.playerHealth;
        slider.value = slider.maxValue;
    }

    void Update()
    {
        slider.value = playerData.playerHealth;
    }
}
