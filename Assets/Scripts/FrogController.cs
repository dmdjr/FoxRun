using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FrogController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float jumpPower = 1f;
    float minThinkTime = 1f; // �ּ� ���� �ð�
    float maxThinkTime = 2f; // �ִ� ���� �ð�
    float thinkTime = 0f; // �������� ������ ���� �ð�
    int objActionNum = 0; // AI�� � �ൿ�� �� �� �����ִ� ����(1�̸� xFlip, 2�̸� �ش� �������� ����)

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
