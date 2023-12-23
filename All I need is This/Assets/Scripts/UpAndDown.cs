using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    public float speed = 1f;
    public float amplitude = 1f;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        float y = Mathf.Sin(Time.time * speed) * amplitude;

        transform.position = new Vector3(initialPosition.x, initialPosition.y + y, initialPosition.z);

    }
}
