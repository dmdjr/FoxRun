using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;

    public LayerMask groundLayer;
    public float groundCheckDistance = 1.2f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveInput = 1;
        }
        else moveInput = 0;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;
        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.green);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

    }
    private void FixedUpdate()
    {
        // 물리 프레임 기준으로 위치 이동
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);


    }
}
