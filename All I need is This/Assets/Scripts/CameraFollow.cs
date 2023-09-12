using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 4f;
    public Transform target; // this is the transform of the player which is being followed

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newPosition, followSpeed * Time.deltaTime);
    }
}
