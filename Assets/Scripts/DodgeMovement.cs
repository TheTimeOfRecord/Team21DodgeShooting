using System;
using UnityEngine;

public class DodgeMovement : MonoBehaviour
{
    private DodgeController controller;
    private Rigidbody2D rb;
    private StatHandler statHandler;

    private Vector2 MoveDirection = Vector2.zero;


    private void Awake()
    {
        controller = GetComponent<DodgeController>();
        rb = GetComponent<Rigidbody2D>();
        statHandler = GetComponent<StatHandler>();
    }

    private void Start()
    {
        controller.OnMoveEvent += Move;
    }

    private void FixedUpdate()
    {
        ApplyMove(MoveDirection);
    }

    private void Move(Vector2 vector)
    {
        MoveDirection = vector;
    }

    //������ �ӵ� �ϴ� 10���� �صξ���. ���� ����� ������ speed�� ��ü ����
    private void ApplyMove(Vector2 vector)
    {
        vector = vector * statHandler.CurrentStat.speed;
        rb.velocity = vector;
    }
}