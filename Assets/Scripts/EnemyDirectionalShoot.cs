using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirectionalShoot : EnemyAI
{
    public List<int> BulletDirection = new List<int>();
    private float attackCooldown;
    // Update is called once per frame
    void Update()
    {
        if (attackCooldown < Time.time && timer < Time.time && target != null)
        {
            for (int i = 0; i < BulletDirection.Count; i++)
            {
                Instantiate(bullet, new Vector3(transform.position.x, transform.position.y), Quaternion.Euler(new Vector3(0,0,BulletDirection[i])));
            }
            attackCooldown = Time.time + 1.5f;
        }
    }
}
