using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    [Header("Required References")]
    public List<DoorController> doors = new List<DoorController>();
    public EnterDetection enterDetector;
    public WaveManager waveManager;

    [Header("Wave Data")]
    public GameObject enemy;
    public List<int> enemiesPerWave = new List<int>(); // should be the same or INCREASING
    public List<Vector3> spawnPoints = new List<Vector3>();
    public List<float> secondsBetweenEnemySpawns = new List<float>(); // should be the same or DECREASING

    public void BeginArea()
    {
        enterDetector.enabled = false; // no more detection, we know the player is here
        enterDetector.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        CloseDoors();

        waveManager.SetCurrentArea(this);
        waveManager.StartCycle();
    }

    public void AreaEnded()
    {
        Debug.Log("All waves in area completed, opening doors...");
        OpenDoors();
    }

    void CloseDoors()
    {
        foreach (DoorController controller in doors)
        {
            controller.CloseDoor();
        }
    }

    void OpenDoors()
    {
        foreach (DoorController controller in doors)
        {
            controller.OpenDoor();
        }
    }
}
