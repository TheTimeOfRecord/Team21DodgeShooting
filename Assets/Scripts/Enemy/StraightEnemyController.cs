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

    private void RotateToTarget(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, 0, rotZ - 90f);
    }
}