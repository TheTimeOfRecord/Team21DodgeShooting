using UnityEngine;

public class StraightEnemyController : EnemyController
{
    private Vector2 direction;

    protected override void Start()
    {
        base.Start();
        direction = DirectionToTarget();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();      
        CallMoveEvent(direction);
        RotateToTarget(direction);
    }
}