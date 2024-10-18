using UnityEngine;

public class StraightEnemyController : EnemyController
{
    private bool isSpawn = false;

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        direction = DirectionToTarget();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();


        if (!isSpawn && gameObject.activeSelf)
        {
            SetDirection();
        }

        RotateToTarget(direction);
        CallMoveEvent(direction);

        float distance = DistanceToTarget();
        if (distance > 25)
        {
            gameObject.SetActive(false);
            isSpawn = false;
        }
    }

    private void SetDirection()
    {
        direction = DirectionToTarget();
        isSpawn = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerArea"))
        {
            gameObject.SetActive(false);
            isSpawn = false;
        }
    }
}