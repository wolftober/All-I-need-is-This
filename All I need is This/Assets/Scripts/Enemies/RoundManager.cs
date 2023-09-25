using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public GameObject spawnedEnemies;

    // the spawn locations, basic
    public Vector3 spawn1 = new Vector3(1, 0, 0);
    public Vector3 spawn2 = new Vector3(0, 1, 0);
    public Vector3 spawn3 = new Vector3(0, 0, 1);
    public Vector3 spawn4 = new Vector3(1, 1, 0);

    List<Vector3> pointList = new List<Vector3>();

    // the current round
    public int round = 1;

    // the ENEMIES
    public GameObject redDude;

    // the things that change as current round number increases
    // the INITIAL SETTINGS
    public int numberOfEnemies = 5;
    public bool bossWave = false; // not utilized yet
    public float spawnCooldown = 1f;
    public float enemyHealthAddition = 0f; // prob not utilized yet

    int currentPointIndex = 0;
    int enemiesLeft;

    // returns a spawn location for the enemy to originate
    // will be made more advanced later
    Vector3 getSpawnPoint()
    {
        Vector3 point = pointList[currentPointIndex];

        if (currentPointIndex == 3)
        {
            currentPointIndex = 0;
            pointList = shuffleList(pointList);
        }

        currentPointIndex++;

        return point;
    }

    // used to shuffle the spawn point list in order for the spawns to rotate around the map randomly
    // and hit each location once
    List<Vector3> shuffleList(List<Vector3> list)
    {
        List<Vector3> temp = new List<Vector3>();
        List<Vector3> shuffledList = new List<Vector3>();
        temp.AddRange(list);

        for (int i = 0; i < list.Count; i++)
        {
            int index = Random.Range(0, temp.Count - 1);
            shuffledList.Add(temp[index]);
            temp.RemoveAt(index);
        }

        return shuffledList;
    }
    
    // spawns enemies based on
    void spawnEnemies()
    {
        int count = 1;

        while (count <= numberOfEnemies)
        {
            //Debug.Log("Spawning Enemy at point : " + spawnPoint);
            GameObject newreddude = Instantiate(redDude, getSpawnPoint(), Quaternion.identity, spawnedEnemies.transform);
            newreddude.SetActive(true);
            // this is where the red dude's health would be increased

            count++;
        }
    }

    // adds to the configuration settings of the round to make it harder
    void makeRoundMoreDifficult()
    {
        // ex : enemycount = 5 * round number
    }

    // should make future round more diffult and open shop
    void intermission()
    {
        Debug.Log("Entered Intermission...");
        makeRoundMoreDifficult();
    }

    public void startRound()
    {
        spawnEnemies();
    }

    public void checkEnemyCount()
    {
        enemiesLeft--;

        if (enemiesLeft == 0)
        {
            intermission();
        }
    }

    void Start()
    {
        // initialize point list
        pointList.Add(spawn1);
        pointList.Add(spawn2);
        pointList.Add(spawn3);
        pointList.Add(spawn4);

        // initialize other stuff
        enemiesLeft = numberOfEnemies;

        // the actual round cycle
        startRound();
    }
}
