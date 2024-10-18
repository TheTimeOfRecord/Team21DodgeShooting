using System;
using UnityEngine;

public class TracingEnemyController : EnemyController
{
    // private bool isCollidingWithTarget;

    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        direction = DirectionToTarget();
        RotateToTarget(direction);
        CallMoveEvent(direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}