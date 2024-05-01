using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomTracker : MonoBehaviour
{
    private DificultyTracker DT;

    private RoomManager RM;

    public GameObject roomManager;

    public Text roomText;

    void Start()
    {
        RM = roomManager.GetComponent<RoomManager>();
        DT = GetComponent<DificultyTracker>();
    }

    void FixedUpdate()
    {
        roomText.text = "Rooms Completed: " + DT.rooms +  "/" + (RM.roomCount - 2);
    }
}
