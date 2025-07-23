using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float moveInput;
    private bool isGrounded;

    public LayerMask groundLayer;
    public float groundCheckDistance = 1.2f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }


    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            spriteRenderer.flipX = true;
            moveInput = -1;
            animator.SetBool("isRun", true);

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            spriteRenderer.flipX = false;
            moveInput = 1;
            animator.SetBool("isRun", true);

        }
        else
        {
            moveInput = 0;
            animator.SetBool("isRun", false);
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.green);
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
           
        }
        // �ִϸ��̼� �Ķ���� ����
        animator.SetBool("isJump", !isGrounded);  // ���߿� �� ������ Jump �ִϸ��̼� ON
    }
    private void FixedUpdate()
    {
        // ���� ������ �������� ��ġ �̵�
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

    }
}
