using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{

    private DificultyTracker DT;

    private RoomManager RM;

    private GameObject roomManager;

    private GameObject player;

    private GameObject healthManager;

    private HealthManager HM;

    private Animator animator;

    void Start()
    {
        roomManager = GameObject.FindGameObjectWithTag("RoomManager");
        player = GameObject.FindGameObjectWithTag("Player");
        healthManager = GameObject.FindGameObjectWithTag("WinPanel");
        RM = roomManager.GetComponent<RoomManager>();
        DT = player.GetComponent<DificultyTracker>();
        HM = healthManager.GetComponent<HealthManager>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (RM.roomCount - 2 == DT.rooms)
        {
            animator.SetBool("TrapDoorOpen", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(player) {
            if (RM.roomCount - 2 == DT.rooms)
            {
                HM.GameWon();
            }
        }
    }
}
