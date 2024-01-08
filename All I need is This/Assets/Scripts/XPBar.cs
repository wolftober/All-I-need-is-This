using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    public PlayerData playerData;

    public Slider xpSlider;
    public TextMeshProUGUI levelLabel;
    public TextMeshProUGUI pointsLabel;

    public void UpdateXPBar()
    {
        xpSlider.maxValue = playerData.xpPointsNeeded;
        xpSlider.value = playerData.xpPoints;

        levelLabel.text = $"Level {playerData.xpLevel}";
        pointsLabel.text = $"{playerData.xpPoints}/{playerData.xpPointsNeeded}";
    }

    private void Start()
    {
        UpdateXPBar();
    }
}
