using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntButtonManager : MonoBehaviour
{
    public GameObject IntermissionObject;
    public GameObject enemyManagerObject;
    public RoundManager rm;

    // The Buttons
    public Button button_Continue;

    void Start()
    {
        rm = enemyManagerObject.GetComponent<RoundManager>();
        button_Continue.onClick.AddListener(continueClicked);
    }

    // should interact with the Round Manager script on the Enemy Manager Object
    // to begin the next round
    void continueClicked()
    {
        // remove the panel
        IntermissionObject.SetActive(false);

        // interact with Round Manager
        rm.startRound();
    }
}
