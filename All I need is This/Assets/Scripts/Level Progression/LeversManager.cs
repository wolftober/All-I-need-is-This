using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeversManager : MonoBehaviour
{
    int leverCount = 0;
    public GameObject gameObjectToDeactivate;

    public void Subscribe()
    {
        leverCount++;
    }

    public void LeverActivated()
    {
        leverCount--;
        if (leverCount == 0)
        {
            gameObjectToDeactivate.SetActive(false);
        }
    }
}
