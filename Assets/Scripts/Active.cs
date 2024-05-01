using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Active : MonoBehaviour
{
    public GameObject RoomManager;

    public GameObject Player;

    public List<GameObject> Rooms = new List<GameObject>();

    private RoomManager RM;

    private DificultyTracker DT;

    private EnemyAI EAI;

    public List<GameObject> Enemies = new List<GameObject>();

    private bool closed = false;

    private int actualRoom;

    private GameObject MinimapController;

    private MiniMapController MMC;
    public void Start()
    {
        foreach (Transform child in transform) 
        { 
            Enemies.Add(child.gameObject); 
        }

        RoomManager = GameObject.FindGameObjectWithTag("RoomManager");
        Player = GameObject.FindGameObjectWithTag("Player");
        MinimapController = GameObject.FindGameObjectWithTag("MiniMapCollector");
        MMC = MinimapController.GetComponent<MiniMapController>();
        RM = RoomManager.GetComponent<RoomManager>();
        DT = Player.GetComponent<DificultyTracker>();
        Rooms.AddRange(GameObject.FindGameObjectsWithTag("Room"));
    }
    public void spawn(int room)
    {
        int difficultyChange = 3 - DT.difficulty;
        Debug.Log(difficultyChange);
        for (int i = 0; i < (Enemies.Count - difficultyChange); i++)
        {
            EAI = Enemies[i].GetComponent<EnemyAI>();
            Enemies[i].SetActive(true);
            EAI.timer = Time.time + 0.8f;
        }
        while (difficultyChange > 0)
        {
            EAI = Enemies[Enemies.Count - 1].GetComponent<EnemyAI>();
            EAI.destroyEnemy();
            Enemies.RemoveAt(Enemies.Count - 1);
            difficultyChange--;
        }
        actualRoom = ((Rooms.Count - 1) - room) - 1;
        RM.CloseDoors(Rooms[actualRoom]);
        MMC.MiniMapHide();
        DT.timerStart();
        closed = true;
    }

    public void Update()
    {
        if(closed == true)
        {
            for (int i = 0; i < Enemies.Count; i++)
                if (Enemies[i] == null)
                    Enemies.RemoveAt(i);
            open();
        }
    }

    private void open() 
    {
        if (Enemies.Count == 0)
        {
            RM.ReOpenDoors(Rooms[actualRoom]);
            DT.timerStop();
            MMC.MiniMapShow();
            closed = false;
        }
    }

    public void newEnemy()
    {
        foreach (Transform child in transform)
        {
            if (!Enemies.Contains(child.gameObject))
                Enemies.Add(child.gameObject);
        }
    }
}
