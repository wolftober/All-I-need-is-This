using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteZone : MonoBehaviour
{
    public LevelLoader levelLoader;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        levelLoader.LoadNextLevel();
    }
}
