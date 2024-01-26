using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    [Header("Required References")]
    public List<DoorController> doors = new List<DoorController>();
    public EnterDetection enterDetector;
    public WaveManager waveManager;

    [Header("Wave Data")]
    public string enemyType = "skeleton";
    public int enemiesPerWave = 2;
    public int waves = 1;

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
