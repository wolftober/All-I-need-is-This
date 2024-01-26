using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    int enemiesAlive = 0;
    int waves;

    AreaManager currentArea;

    bool areaStarted = false;

    public void SetCurrentArea(AreaManager area)
    {
        currentArea = area;
        waves = currentArea.waves;
        Debug.Log($"Area: {waves} waves, {currentArea.enemiesPerWave} enemies per wave");
    }

    public void StartCycle()
    {
        if (!areaStarted)
        {
            areaStarted = true;
            StartNextWave();
        }
    }

    public void StartNextWave()
    {
        Debug.Log($"Spawning {currentArea.enemiesPerWave} enemies.");
        for (int i = 0; i < currentArea.enemiesPerWave; i++)
        {
            
        }
        enemiesAlive = currentArea.enemiesPerWave;
    }

    public void AnEnemyHasDied()
    {
        enemiesAlive--;
        Debug.Log($"An enemy has died, {enemiesAlive} enemies left.");
        if (enemiesAlive == 0) 
        {
            waves--;
            Debug.Log($"Waves left is now: {waves}");
            if (waves == 0)
            {
                currentArea.AreaEnded();
            }
        }
    }
}
