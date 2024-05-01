using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject bottomDoor;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;

    [SerializeField] GameObject topDoorClose;
    [SerializeField] GameObject bottomDoorClose;
    [SerializeField] GameObject leftDoorClose;
    [SerializeField] GameObject rightDoorClose;
    public Vector2Int RoomIndex { get; set; }

    public void OpenDoor(Vector2Int direction)
    {
        if (direction == Vector2Int.up)
        {
            topDoor.SetActive(false);
        }

        if (direction == Vector2Int.down)
        {
            bottomDoor.SetActive(false);
        }

        if (direction == Vector2Int.left) { 

            leftDoor.SetActive(false);
        }

        if (direction == Vector2Int.right)
        {
            rightDoor.SetActive(false);
        }
    }

    public void CloseDoor()
    {
        topDoorClose.SetActive(true);
        bottomDoorClose.SetActive(true);
        leftDoorClose.SetActive(true);
        rightDoorClose.SetActive(true);
    }

    public void reopen()
    {
        topDoorClose.SetActive(false);
        bottomDoorClose.SetActive(false);
        leftDoorClose.SetActive(false);
        rightDoorClose.SetActive(false);
    }
}
