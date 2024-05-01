using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet2 : MonoBehaviour
{
    Rigidbody2D rb;
    private static float bulletSpeed;
    void Start()
    {
        bulletSpeed = PlayerShoot.bulletSpeed;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Door") || collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}