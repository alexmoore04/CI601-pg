using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerEnemy : EnemyAI
{
    private float spawnClock;

    public GameObject enemySpawned;

    public Active enemyTracker;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
        spawnClock = Time.time + 4;
        enemyTracker = GetComponentInParent<Active>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnClock < Time.time && target != null)
        {
            Instantiate(enemySpawned, transform.position, Quaternion.identity, enemyTracker.transform);
            enemyTracker.newEnemy();
            spawnClock = Time.time + 4;
        }
    }
}
