using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveScript : MonoBehaviour
{
    
    public SaveData saveData = new SaveData();
    private DificultyTracker DT;
    private int saveChecker;

    [SerializeField] private int version;

    private void Start()
    {
        GameObject PC = GameObject.Find("Player");
        DT = PC.GetComponent<DificultyTracker>();
        saveChecker = DT.saveUpdater;
    }

    private void Update()
    {
        if (saveChecker < DT.saveUpdater)
        {
            SaveToJson();
            saveChecker = DT.saveUpdater;
        }
    }

    public void SaveToJson()
    {
        UpdateSaveData();
        string data = JsonUtility.ToJson(saveData);
        string filePath = Application.persistentDataPath + "/SaveData.json";
        Debug.Log("FilePath: " + filePath);
        System.IO.File.WriteAllText(filePath, data);
        Debug.Log("Data Saved");
    }

    public void loadFromJson()
    {
        string filePath = Application.persistentDataPath + "/SaveData.json";
        string save = System.IO.File.ReadAllText(filePath);

        if (save.Length > 1)
        {
            saveData = JsonUtility.FromJson<SaveData>(save);
            Debug.Log("Data Loaded");
        }
    }

    public void UpdateSaveData()
    {
        //saveData.candidateNumber;
        saveData.version.Add(version);
        saveData.room.Add(DT.rooms);
        saveData.dificulty.Add(DT.difficulty);
        saveData.dificultyTracker.Add(DT.difficultyCalculate);
        saveData.health.Add(DT.health);
        saveData.healthLostInPrevRoom.Add(DT.healthLost);
        saveData.avgSpeed.Add(DT.avgSpeed);
        saveData.prevSpeed.Add(DT.prevTime);
        saveData.speedScore.Add(DT.speedScore);
        saveData.avgShots.Add(DT.avgShotsFired);
        saveData.prevShots.Add(DT.prevShotsFired);
        saveData.shotScore.Add(DT.shotScore);
    }

}

public class SaveData
{
    //public int candidateNumber;
    public List<int> version = new List<int>();
    public List<float> room = new List<float>();
    public List<int> dificulty = new List<int>();
    public List<float> dificultyTracker = new List<float>();
    public List<int> health = new List<int>();
    public List<int> healthLostInPrevRoom = new List<int>();
    public List<float> avgSpeed = new List<float>();
    public List<float> prevSpeed = new List<float>();
    public List<float> speedScore = new List<float>();
    public List<float> avgShots = new List<float>();
    public List<float> prevShots = new List<float>();
    public List<float> shotScore = new List<float>();
}
