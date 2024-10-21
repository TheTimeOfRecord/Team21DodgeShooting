using UnityEngine;

public class StraightEnemyController : EnemyController
{
    private bool isSpawn;

    protected override void OnEnable()
    {
        base.OnEnable();
        isSpawn = true;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (isSpawn)
        {
            isSpawn = false;
            direction = DirectionToTarget();            
        }

        RotateToTarget(direction);
        CallMoveEvent(direction);

        float distance = DistanceToTarget();
        if (distance > 15)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerArea"))
        {
            gameObject.SetActive(false);
        }
    }
}