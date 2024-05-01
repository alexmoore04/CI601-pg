using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    private bool wait = true;

    public float speed = 2f;
    protected float speedHold;
    protected float speedTime;

    public float health = 5f;
    public float timer = 0;
    public float minDistance = 0f;
    private float range;

    public bool spawner;

    private SpriteRenderer sr;

    public GameObject PC;

    public PlayerShoot PS;

    public GameObject bullet;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
        PC = GameObject.Find("Player");
        speedHold = speed;
        sr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (!wait && target != null)
        {
            range = Vector2.Distance(transform.position, target.position);
            if (range > minDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }
        else
        {
            if (timer < Time.time)
            {
                wait = false;
            }
        }
        if (speed < speedHold && speedTime < Time.time)
        {
            speed = speed + 0.4f;
            speedTime = Time.time + 0.05f;
        }
    }

    void FixedUpdate()
    {
        if (!spawner && target != null)
            sr.flipX = target.position.x < transform.position.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            health = health - 0.5f;
            speed = 1;
        }

        if (collision.CompareTag("Player"))
        {
            health = health - 0.5f;
            speed = 1;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void destroyEnemy()
    {
        Destroy(gameObject);
    }
}
