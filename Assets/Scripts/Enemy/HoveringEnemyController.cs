﻿using System;
using UnityEngine;

public class HoveringEnemyController : EnemyController
{
    [SerializeField, Range(1f, 5f)] private float Range;
    private float rad;
    private bool isRanged;
    private Vector2 initDirectionInRange;

    protected override void OnEnable()
    {
        base.OnEnable();
        isRanged = false;
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

        if (!isRanged)
        {
            float distance = DistanceToTarget();
            CallMoveEvent(direction);

            if (distance <= Range)
            {
                isRanged = true;
                initDirectionInRange = direction * -1;
            }
        }
        else
        {
            direction = direction * -1;
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(0, 0, rotZ);
            MoveCircle();
        }
    }

    private void MoveCircle()
    {
        rad += Time.deltaTime * (stats.CurrentStat.speed/Range);
        float initialRad = Mathf.Atan2(initDirectionInRange.y, initDirectionInRange.x);
        float x = Range * Mathf.Cos(rad + initialRad);
        float y = Range * Mathf.Sin(rad + initialRad);
        transform.position = AttackTarget.position + new Vector3(x, y);
        if (rad >= Mathf.PI * 2) rad = 0;
    }
}