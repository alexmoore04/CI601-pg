using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DificultyTracker : MonoBehaviour
{
    public int saveUpdater = 0;

    public int difficulty = 1;

    public float difficultyCalculate = 1f;

    public bool gameMode;

    public bool damageCheck = false;

    private bool stopCheck = false;

    private float iFrames = 0;

    private float Timer = 0;

    public float shotScore = 0;

    public float speedScore = 0;

    private int difficultyCap = 0;

    [SerializeField] private int difficultyMover = 3;

    [SerializeField] public float avgSpeed;

    public float rooms;

    [SerializeField] public float prevTime;

    [SerializeField] public float prevShotsFired;

    [SerializeField] public float avgShotsFired;

    public int health = 10;

    private int startHealth = 10;

    [SerializeField] public int healthLost = 0;

    private int shots = 0;

    PlayerShoot shotsCollect;

    private void Start()
    {
        GameObject PC = GameObject.Find("Player");
        shotsCollect = PC.GetComponent<PlayerShoot>();
    }

    public void timerStart()
    {
        Timer = Time.time;
        startHealth = health;
        shots = shotsCollect.bulletsFired;
    }

    public void timerStop()
    {
        prevTime = Time.time - Timer;
        rooms = rooms + 1;
        avgSpeed = (avgSpeed * (rooms - 1) + prevTime) / rooms;
        healthLost = startHealth - health;
        if (!stopCheck)
            for (int i = 1; i < healthLost; i++)
                if (difficulty == 3)
                {
                    difficultyCap++;
                    if (difficultyCap >= 3)
                    {
                        difficultyMover = 2;
                        stopCheck = true;
                    }
                }
        prevShotsFired = shotsCollect.bulletsFired - shots;
        avgShotsFired = (avgShotsFired * (rooms - 1) + prevShotsFired) / rooms;
        if (gameMode)
            updateDificulty();
        else
            randomDifficulty();
    }

    private void Update()
    {
        if (damageCheck)
        {
            if (iFrames < Time.time)
            {
                health--;
                iFrames = Time.time + 1.5f;
            }
            damageCheck = false;
        }
    }

    private void updateDificulty()
    {
        if (prevShotsFired > 25)
            shotScore = calculation(shotScore, prevShotsFired, -0.1f, 25);
        else
            shotScore = calculation(shotScore, prevShotsFired, 0.1f, 25);
        if (prevTime > 17)
            speedScore = calculation(speedScore, prevTime, -0.1f, 17);
        else
            speedScore = speedScore = calculation(speedScore, prevTime, 0.1f, 17);
        difficultyCalculate = Mathf.Clamp((difficultyCalculate + (speedScore + shotScore)) , 0 , difficultyMover) - (healthLost * 0.5f);
        if (difficultyCalculate <= 1)
            difficulty = 1;
        else if (difficultyCalculate <= 2)
            difficulty = 2;
        else
            difficulty = 3;
        saveUpdater++;
        Debug.Log("Difficulty calculation = " + difficultyCalculate + ". Difficulty = " + difficulty + ". Shot score = " + shotScore + ". speed score = " + speedScore);
    }

    private float calculation(float score, float prev, float vari, int amount)
    {
        if (vari < 0)
            return Mathf.Clamp((score + ((prev - amount) * vari)) / 2, -1, 1);
        else
            return Mathf.Clamp((score  + ((amount - prev) * vari)) / 2, -1, 1);
    }

    private void randomDifficulty()
    {
        difficultyCalculate = Random.Range(1, 4);
        if (difficultyCalculate <= 1)
            difficulty = 1;
        else if (difficultyCalculate <= 2)
            difficulty = 2;
        else
            difficulty = 3;
        Debug.Log("Difficulty calculation = " + difficultyCalculate + ". Difficulty = " + difficulty);
        saveUpdater++;
    }
}
