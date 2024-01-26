using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public void CloseDoor()
    {
        gameObject.SetActive(true);
    }

    public void OpenDoor()
    {
        gameObject.SetActive(false);
    }
}
