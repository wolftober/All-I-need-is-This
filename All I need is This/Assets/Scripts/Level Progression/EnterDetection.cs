using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDetection : MonoBehaviour
{
    public AreaManager areaManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        areaManager.BeginArea();
    }
}
