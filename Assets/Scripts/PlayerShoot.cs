using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public static float bulletSpeed = 400;
    public float fireRate = 0.5f;
    public float damage = 1;
    public int bulletsFired = 0;

    private SpriteRenderer sr;

    private Rigidbody2D PCRB;
    private float fireTimer = 0;
    private void Start()
    {
        PCRB = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if (fireTimer < Time.time)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Instantiate(bullet, new Vector3(PCRB.transform.position.x, PCRB.transform.position.y + 0.5f), Quaternion.Euler(0, 0, 0 - PCRB.velocity.x));
                fireTimer = Time.time + fireRate;
                bulletsFired++;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Instantiate(bullet, new Vector3(PCRB.transform.position.x + 0.5f, PCRB.transform.position.y), Quaternion.Euler(0, 0, 270 + PCRB.velocity.y));
                fireTimer = Time.time + fireRate;
                bulletsFired++;
                sr.flipX = false;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Instantiate(bullet, new Vector3(PCRB.transform.position.x, PCRB.transform.position.y - 0.5f), Quaternion.Euler(0, 0, 180 + PCRB.velocity.x));
                fireTimer = Time.time + fireRate;
                bulletsFired++;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                Instantiate(bullet, new Vector3(PCRB.transform.position.x - 0.5f, PCRB.transform.position.y), Quaternion.Euler(0, 0, 90 - PCRB.velocity.y));
                fireTimer = Time.time + fireRate;
                bulletsFired++;
                sr.flipX = true;
            }
            else
            {
                sr.flipX = PCRB.velocity.x < 0;
            }
        }
    }
}
