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

    private int jumpCount = 0; // 현재 점프 횟수
    public int maxJumpCount = 2; // 최대 점프 횟수 (2단 점프)



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
            if (!isGrounded) // 착지한 순간
            {
                jumpCount = 0; // 점프 카운트 초기화
            }
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f); // 점프 누적 방지 (2단 점프 시 수직 속도 초기화)
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
            // Jump 애니메이션을 강제로 처음부터 재생
            animator.Play("Player_Jump", 0, 0f);
        }
        // 애니메이션 파라미터 갱신
        animator.SetBool("isJump", !isGrounded);  // 공중에 떠 있으면 Jump 애니메이션 ON
    }
    private void FixedUpdate()
    {
        // 물리 프레임 기준으로 위치 이동
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

    }
}
