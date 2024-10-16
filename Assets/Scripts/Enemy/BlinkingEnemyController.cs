using System.Collections;
using UnityEngine;

public class BlinkingEnemyController : EnemyController
{
    private bool canBlink;

    protected override void Start()
    {
        base.Start();
        canBlink = true;
    }

    protected override void FixedUpdate()
    {
        Vector2 direction = DirectionToTarget();
        RotateToTarget(direction);

        if (canBlink)
        {
            StartCoroutine(BlinkCoroutine());
        }
    }

    IEnumerator BlinkCoroutine()
    {
        Blink();
        canBlink = false;
        yield return new WaitForSeconds(3f);
        canBlink = true;
    }

    private void Blink()
    {
        float x;
        float y;
        while (true)
        {
            x = Random.Range(-5f, 5f);
            y = Random.Range(-3f, 7.5f);

            if ((x > -1 && x < 1) && (y > -1 && y < 1))
            {
                continue;
            }
            else
            {
                break;
            }
        }
        transform.position = PlayerTransform.position + new Vector3(x, y, 0f);
    }
}