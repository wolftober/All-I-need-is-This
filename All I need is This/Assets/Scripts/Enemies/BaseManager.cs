using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    // positions of enemy bases around the map
    public Vector3 base1 = new Vector3();
    public Vector3 base2 = new Vector3();
    public Vector3 base3 = new Vector3();
    public Vector3 base4 = new Vector3();

    // returns the position of the closest base to the enemy
    public Vector3 getTargetPosition(Vector3 enemyPos)
    {
        Vector3 closestBasePos = base1;

        if (Vector3.Distance(base2, enemyPos) < Vector3.Distance(base1, enemyPos))
        {
            closestBasePos = base2;
        }
        if (Vector3.Distance(base3, enemyPos) < Vector3.Distance(base2, enemyPos))
        {
            closestBasePos = base3;
        }
        if (Vector3.Distance(base4, enemyPos) < Vector3.Distance(base3, enemyPos))
        {
            closestBasePos = base4;
        }

        // temp
        return closestBasePos;
    }
}
