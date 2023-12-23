using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BottomUIUpdate : MonoBehaviour
{
    public PlayerData playerData;

    public Slider healthSlider; // health

    public Slider xpSlider; // xp
    public TextMeshProUGUI xpPointsLabel;
    public TextMeshProUGUI levelLabel;

    public TextMeshProUGUI coinLabel; // coins


    private void Start()
    {
        // health
        healthSlider.maxValue = playerData.playerHealth;
        healthSlider.value = healthSlider.maxValue;

        // xp
        xpSlider.maxValue = playerData.xpPointsNeeded;
        xpSlider.value = playerData.xpPoints;
    }

    void Update()
    {
        // health
        healthSlider.value = playerData.playerHealth;

        // xp
        float currentPoints = playerData.xpPoints;
        float pointsNeeded = playerData.xpPointsNeeded;

        xpSlider.value = currentPoints;
        xpSlider.maxValue = pointsNeeded;
        xpPointsLabel.text = currentPoints.ToString() + '/' + pointsNeeded;
        levelLabel.text = "Level " + playerData.xpLevel.ToString();

        // coins
        coinLabel.text = playerData.coins.ToString();
    }
}
