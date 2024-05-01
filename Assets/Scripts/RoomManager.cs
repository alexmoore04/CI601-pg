using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject roomPrefab;
    [SerializeField] private int maxRooms = 15;
    [SerializeField] private int minRooms = 10;
    [SerializeField] GameObject[] regRooms;
    [SerializeField] GameObject startRoom;
    [SerializeField] GameObject[] bossRooms;
    [SerializeField] GameObject saveData;

    private SaveScript loadData;

    int roomWidth = 18;
    int roomHeight = 10;

    [SerializeField] int gridSizeX = 10;
    [SerializeField] int gridSizeY = 10;

    private List<GameObject> roomObjects = new List<GameObject>();

    public List<int> roomX = new List<int>();
    public List<int> roomY = new List<int>();

    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();

    private int[,] roomGrid;

    public int roomCount;

    private bool generationComplete = false;

    private Checker enemyCollect;

    Checker cameraC;

    private void Start()
    {
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue = new Queue<Vector2Int>();

        cameraC = FindObjectOfType<Checker>();

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);

        GameObject PC = GameObject.Find("Player");
        enemyCollect = PC.GetComponent<Checker>();

        loadData = saveData.GetComponent<SaveScript>();
    }

    private void StartRoomGenerationFromRoom(Vector2Int roomIndex)
    {
        roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;
        roomGrid[x, y] = 1;
        roomCount++;
        var initialRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        initialRoom.name = $"Room-{roomCount}";
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);

    }

    private void Update()
    {
        if (roomQueue.Count > 0 && roomCount < maxRooms && !generationComplete)
        {
            Vector2Int roomIndex = roomQueue.Dequeue();
            int gridX = roomIndex.x;
            int gridY = roomIndex.y;

            TryGenerateRoom(new Vector2Int(gridX - 1, gridY));
            TryGenerateRoom(new Vector2Int(gridX + 1, gridY));
            TryGenerateRoom(new Vector2Int(gridX, gridY + 1));
            TryGenerateRoom(new Vector2Int(gridX, gridY - 1));
        }
        else if (roomCount < minRooms)
        {
            Debug.Log("RoomCount was less than the minimum ammount of rooms. trying again");
            RegenerateRooms();
        }
        else if (!generationComplete)
        {
            Debug.Log($"Generation complete, {roomCount} rooms created");
            generationComplete = true;
            cameraC.CameraCollecter();
            SetRoomType();
        }
    }

    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        if (roomCount >= maxRooms)
            return false;

        if (Random.value < 0.5f && roomIndex != Vector2Int.zero)
            return false;

        if (CountAdjacentRooms(roomIndex) > 1)
            return false;

        if (roomGrid[x, y] == 1)
            return false;    

        roomQueue.Enqueue(roomIndex);
        roomX.Add(x);
        roomY.Add(y);
        roomGrid[x, y] = 1;
        roomCount++;
        var newRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        newRoom.GetComponent<Room>().RoomIndex = roomIndex;
        newRoom.name = $"Room-{roomCount}";
        roomObjects.Add(newRoom);

        OpenDoors(newRoom, x, y, 0);

        Debug.Log($"Generation complete, {roomCount} rooms created");
        return true;
    }

    // Clear all rooms and try again
    private void RegenerateRooms()
    {
        roomObjects.ForEach(Destroy);
        roomObjects.Clear();
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue.Clear();
        roomCount = 0;
        generationComplete = false;

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    public void OpenDoors(GameObject room, int x, int y, int roomnum)
    {
        Room newRoomScript = room.GetComponent<Room>();

        //Neighbours
        Room leftRoomScript = GetRoomScriptAt(new Vector2Int(x - 1, y));
        Room rightRoomScript = GetRoomScriptAt(new Vector2Int(x + 1, y));
        Room topRoomScript = GetRoomScriptAt(new Vector2Int(x, y + 1));
        Room bottomRoomScript = GetRoomScriptAt(new Vector2Int(x, y - 1));

            // Determine which doors to open based on the direction
            if (x > 0 && roomGrid[x - 1, y] != 0)
            {
                // Neighbouring room to the left
                newRoomScript.OpenDoor(Vector2Int.left);
                leftRoomScript.OpenDoor(Vector2Int.right);
            }
            if (x < gridSizeX && roomGrid[x + 1, y] != 0)
            {
            // Neighbouring room to the right
            newRoomScript.OpenDoor(Vector2Int.right);
            rightRoomScript.OpenDoor(Vector2Int.left);
        }
            if (y > 0 && roomGrid[x, y - 1] != 0)
            {
            // Neighbouring room bellow
            newRoomScript.OpenDoor(Vector2Int.down);
            bottomRoomScript.OpenDoor(Vector2Int.up);
        }
            if (y < gridSizeY && roomGrid[x, y + 1] != 0)
            {
            // Neighbouring room above
            newRoomScript.OpenDoor(Vector2Int.up);
            topRoomScript.OpenDoor(Vector2Int.down);
        }

    }

    public void CloseDoors(GameObject room)
    {
        Room newRoomScript = room.GetComponent<Room>();
        newRoomScript.CloseDoor();
    }

    public void ReOpenDoors(GameObject room)
    {
        Room newRoomScript = room.GetComponent<Room>();
        newRoomScript.reopen();
    }

    Room GetRoomScriptAt(Vector2Int index)
    {
        GameObject roomObject = roomObjects.Find(r => r.GetComponent<Room>().RoomIndex == index);
        if (roomObject != null)
            return roomObject.GetComponent<Room>();
        return null;
    }

    private int CountAdjacentRooms(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0;

        if (x > 0 && roomGrid[x - 1, y] != 0) count++; // Left neighbour
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0) count++; // Right neighbour
        if (y > 0 && roomGrid[x, y - 1] != 0) count++; // Bottom neighbour
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0) count++; // Top neighbour

        return count;
    }

    private int CountAdjacentRoomsFinish(Vector2Int roomPos)
    {
        int x = roomPos.x;
        int y = roomPos.y;
        int count = 0;
        for (int i = 0; i < roomObjects.Count; i++)
        {
            if (roomObjects[i].transform.position == new Vector3(x - 18, y)){ count++; } // Left neighbour
            if (roomObjects[i].transform.position == new Vector3(x + 18, y)){ count++; } // Right neighbour
            if (roomObjects[i].transform.position == new Vector3(x, y - 10)){ count++; } // Bottom neighbour
            if (roomObjects[i].transform.position == new Vector3(x, y + 10)){ count++; } // Top neighbour
        }
        return count;
    }

    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex)
    {
        int gridX = gridIndex.x;
        int gridY = gridIndex.y;
        return new Vector3(roomWidth * (gridX - gridSizeX / 2), roomHeight * (gridY - gridSizeY / 2));
    }

    private void OnDrawGizmos()
    {
        Color gizmoColor = new Color(0, 1, 1, 0.05f);
        Gizmos.color = gizmoColor;

        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y= 0; y < gridSizeY; y++)
            {
                Vector3 position = GetPositionFromGridIndex(new Vector2Int(x, y));
                Gizmos.DrawWireCube(position, new Vector3(roomWidth, roomHeight, 1));
            }
        }
    }

    private void SetRoomType()
    {
        RoomSetter(0, 0);
        RoomSetter(2, roomObjects.Count - 1);
        for (int i = roomObjects.Count - 2; i > 0; i--)
        {
            RoomSetter(1, i);
            if (CountAdjacentRoomsFinish(new Vector2Int((int)roomObjects[i].transform.position.x, (int)roomObjects[i].transform.position.y)) == 1)
            {
                Debug.Log("potential item room at" + roomObjects[i].transform.position);
            }
        }
        enemyCollect.enemyCollecter();
        loadData.loadFromJson();
    }

    private void RoomSetter(int roomType, int roomObject)
    {
        if (roomType == 0)
        {
            Instantiate(startRoom, roomObjects[0].transform);
        }
        if (roomType == 1) 
        {           
            Instantiate(regRooms[Random.Range(0, regRooms.Length)], roomObjects[roomObject].transform);
        }
        if (roomType == 2)
        {
            Instantiate(bossRooms[Random.Range(0, bossRooms.Length)], roomObjects[roomObject].transform);
        }
    }
}
