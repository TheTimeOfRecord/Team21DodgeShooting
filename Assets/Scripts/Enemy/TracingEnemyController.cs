using System;
using UnityEngine;

public class TracingEnemyController : EnemyController
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }

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
}