using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntButtonManager : MonoBehaviour
{
    public GameObject IntermissionObject;

    // The Buttons
    public Button button_Continue;

    void Start()
    {
        button_Continue.onClick.AddListener(continueClicked);
    }

    void continueClicked()
    {
        IntermissionObject.SetActive(false);
    }
}
