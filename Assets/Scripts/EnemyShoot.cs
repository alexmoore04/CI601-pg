using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : EnemyAI
{
    private float attackCooldown;
    void Update()
    {
        if (attackCooldown < Time.time && timer < Time.time && target != null)
        {
            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            attackCooldown = Time.time + 2f;
        }
    }
}
