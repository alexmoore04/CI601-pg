using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceMove : EnemyAI
{
    public float dirX;
    public float dirY;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        speedHold = speed;
    }

    void Update()
    {
        if (timer < Time.time)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + dirX, transform.position.y + dirY), speed * Time.deltaTime);
        }
        if (speed < speedHold && speedTime < Time.time)
        {
            speed = speed + 0.4f;
            speedTime = Time.time + 0.05f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Door") || collision.CompareTag("Wall") || collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            if (Random.Range(0, 20) > 10)
            {
                dirX = -dirX;
                sr.flipX = dirX < 0;
            }
            if (Random.Range(0, 20) > 10)
                dirY = -dirY;
        }
    }
}
