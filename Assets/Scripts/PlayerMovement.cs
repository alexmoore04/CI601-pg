using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    public float speedMod = 1f;
    private Rigidbody2D rb;
    private Vector2 direction;
    private Vector2 check = new Vector2(0, 0);
    private Animator animator;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        speed = 7f * speedMod;
        if (direction.x > 0.78 || direction.x < -0.78)
        {
            if (direction.y > 0.78 || direction.y < -0.78)
            {
                speed = 5.46f * speedMod;
            }
        }
        rb.velocity = direction * speed;
        if (rb.velocity != check)
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }
    }
}
