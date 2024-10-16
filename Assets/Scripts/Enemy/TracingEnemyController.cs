using System;
using UnityEngine;

public class TracingEnemyController : EnemyController
{
    private bool isCollidingWithTarget;

    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        Vector2 direction = DirectionToTarget();
        CallMoveEvent(direction);
        RotateToTarget(direction);
    }
}