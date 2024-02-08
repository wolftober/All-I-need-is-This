using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public TextMeshProUGUI waveIndicatorLabel;

    int enemiesAlive = 0;
    int waveIndex = 0;

    AreaManager currentArea;

    bool areaStarted = false;

    public void SetCurrentArea(AreaManager area)
    {
        waveIndex = 0;
        currentArea = area;
        Debug.Log($"Area: {currentArea.enemiesPerWave.Count} waves, {currentArea.enemiesPerWave[waveIndex]} in first wave");
        waveIndicatorLabel.text = "";
    }

    public void StartCycle()
    {
        if (!areaStarted)
        {
            areaStarted = true;
            Debug.Log("test");
            StartCoroutine(StartNextWave());
        }
    }


    public IEnumerator StartNextWave()
    {
        waveIndicatorLabel.text = $"Wave {waveIndex + 1}: {currentArea.enemiesPerWave[waveIndex]} Enemies Left";
        Debug.Log($"Spawning {currentArea.enemiesPerWave[waveIndex]} enemies.");
        for (int i = 0; i < currentArea.enemiesPerWave[waveIndex]; i++)
        {
            StartCoroutine(SpawnEnemyWithDelay());
            yield return new WaitForSeconds(currentArea.secondsBetweenEnemySpawns[waveIndex]);
        }
    }

    private IEnumerator SpawnEnemyWithDelay()
    {
        yield return new WaitForSeconds(currentArea.secondsBetweenEnemySpawns[waveIndex]);

        Vector3 randomSpawnPoint = currentArea.spawnPoints[Random.Range(0, currentArea.spawnPoints.Count - 1)];

        GameObject spawnedEnemy = Instantiate(currentArea.enemy, transform);
        spawnedEnemy.transform.position = randomSpawnPoint + new Vector3(0, 0, -1);
        EnemyScript enemyScript = spawnedEnemy.GetComponent<EnemyScript>();
        enemyScript.waveManager = this;
        enemyScript.player = GameObject.FindGameObjectWithTag("Player").transform;

        Debug.Log("Instantiated");
        enemiesAlive++;
    }

    public void AnEnemyHasDied()
    {
        enemiesAlive--;
        Debug.Log($"An enemy has died, {enemiesAlive} enemies left.");
        waveIndicatorLabel.text = $"Wave {waveIndex + 1}: {enemiesAlive} Enemies Left";
        if (enemiesAlive == 0) 
        {
            waveIndex++;
            Debug.Log($"Waves left is now: {currentArea.enemiesPerWave.Count - waveIndex}");
            if (waveIndex >= currentArea.enemiesPerWave.Count)
            {
                currentArea.AreaEnded();
                StartCoroutine(RemoveWaveIndicatorAfterSeconds());
            }
            else
            {
                StartCoroutine(StartNextWave());
            }
        }
    }

    private IEnumerator RemoveWaveIndicatorAfterSeconds()
    {
        waveIndicatorLabel.text = "Area Complete, doors are open";
        yield return new WaitForSeconds(5);
        waveIndicatorLabel.text = "";
    }
}
