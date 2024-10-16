using System;
using UnityEngine;

public class TracingEnemyController : EnemyController
{
    [SerializeField] private string targetTag = "Player";
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

    private void RotateToTarget(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, 0, rotZ - 90f);
    }
}