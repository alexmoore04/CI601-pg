using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    public Camera[] allCameras;

    public List<GameObject> cameraColliders = new List<GameObject>();

    public int currentCamera;

    public List<GameObject> detectors = new List<GameObject>();

    private List<bool> spawned = new List<bool>();

    private bool enemySpawned = false;

    private List<Active> enemyActive = new List<Active>();

    public List<GameObject> enemy = new List<GameObject>();

    private DificultyTracker hp;


    private void Start()
    {
        hp = GetComponent<DificultyTracker>();
    }

    public void CameraCollecter()
    {
        allCameras = Camera.allCameras;
        cameraColliders.AddRange(GameObject.FindGameObjectsWithTag("CameraCollider"));

        for (var i = 0; i < allCameras.Length; ++i)
        {
            if(!allCameras[i].gameObject.CompareTag("MiniMapCam"))
                allCameras[i].enabled = false;
        }
        currentCamera = 0;
        allCameras[currentCamera].enabled = true;

    }

    public void enemyCollecter()
    {
        detectors.AddRange(GameObject.FindGameObjectsWithTag("Detector"));
        enemy.AddRange(GameObject.FindGameObjectsWithTag("EnemyGroup"));
        for (var i = 0; i < detectors.Count; ++i)
        {
            spawned.Add(enemySpawned);
            enemyActive.Add(enemy[i].GetComponent<Active>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CameraCollider"))
        {
            for (var i = 0; i < allCameras.Length - 1; ++i)
            {
                if (collision.gameObject == cameraColliders[i])
                {
                    if (allCameras[i] != allCameras[currentCamera])
                    {
                        allCameras[i].enabled = true;
                        allCameras[currentCamera].enabled = false;
                        currentCamera = i;
                        break;
                    }
                }
            }
        }
        if (collision.CompareTag("Detector"))
        {
            for(int i = 0; i < detectors.Count; i++)
            {
                if (collision.gameObject == detectors[i])
                {
                    if (!spawned[i])
                    {
                        enemyActive[i].spawn(i);
                        spawned[i] = true;
                        break;
                    }
                }
            }
        }
        if (collision.CompareTag("EnemyBullet") || collision.CompareTag("Enemy"))
        {
            hp.damageCheck = true;
        }
        
    }
}
