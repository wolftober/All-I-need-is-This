using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverButtons : MonoBehaviour
{
    public GameObject GameOverPanel;

    // The Buttons
    public Button button_ReturnToMenu;

    void Start()
    {
        button_ReturnToMenu.onClick.AddListener(returnClicked);
    }

    void returnClicked()
    {
        Debug.Log("Return to Menu Scene");
    }
}
