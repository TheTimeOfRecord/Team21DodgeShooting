using System;
using UnityEngine;

public class DodgeMovement : MonoBehaviour
{
    private DodgeController controller;
    private Rigidbody2D rb;

    private Vector2 MoveDirection = Vector2.zero;


    private void Awake()
    {
        controller = GetComponent<DodgeController>();
        rb = GetComponent<Rigidbody2D>();
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

    //움직임 속도 일단 10으로 해두었음. 스탯 만들면 스탯의 speed로 교체 예정
    private void ApplyMove(Vector2 vector)
    {
        vector = vector * 10;
        rb.velocity = vector;
    }
}