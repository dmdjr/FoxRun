using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FrogController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float jumpPower = 1f;
    float minThinkTime = 1f; // 최소 생각 시간
    float maxThinkTime = 2f; // 최대 생각 시간
    float thinkTime = 0f; // 랜덤으로 정해질 생각 시간
    int objActionNum = 0; // AI가 어떤 행동을 할 지 정해주는 숫자(1이면 xFlip, 2이면 해당 방향으로 점프)

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        Think();
    }

    void Think()
    {
        thinkTime = Random.Range(minThinkTime, maxThinkTime);
        objActionNum = Random.Range(1, 11);
        if (objActionNum >= 1 && objActionNum < 8)
        {
            spriteRenderer.flipX = true;
            rb.velocity = new Vector2(moveSpeed, jumpPower);
        }
        else if (objActionNum >= 8 && objActionNum < 11)
        {
            spriteRenderer.flipX = false;
            rb.velocity = new Vector2(-moveSpeed, jumpPower);
        }

        Invoke("Think", thinkTime);
    }
}
