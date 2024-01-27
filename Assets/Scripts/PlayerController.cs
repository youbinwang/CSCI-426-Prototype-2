using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    private bool facingRight = true;
    
    public float umbrellaSpeed = 3f;
    public GameObject umbrella;
    private bool umbrellaActive = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        umbrella.SetActive(false);
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            umbrellaActive = !umbrellaActive;
            umbrella.SetActive(umbrellaActive);
        }
        
        float currentSpeed = umbrellaActive ? umbrellaSpeed : speed;
        moveVelocity = new Vector2(moveInput * currentSpeed, rb.velocity.y);

        if ((moveInput > 0 && !facingRight) || (moveInput < 0 && facingRight))
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    void Flip()
    {
        facingRight = !facingRight;
        
        transform.Rotate(0f, 180f, 0f);
    }
}
