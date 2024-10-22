using System.Collections;
using UnityEngine;

public class BlinkingEnemyController : EnemyController
{
    private bool canBlink;
    [SerializeField] private float blinkDelayTime;

    protected override void OnEnable()
    {
        base.OnEnable();
        canBlink = true;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        direction = DirectionToTarget();
        RotateToTarget(direction);

        if (canBlink)
        {
            canBlink = false;
            StartCoroutine(BlinkCoroutine());
        }
    }

    IEnumerator BlinkCoroutine()
    {
        Blink();
        yield return new WaitForSeconds(blinkDelayTime);
        canBlink = true;
    }

    private void Blink()
    {
        float x = Random.Range(-5f, 5f);
        float y = Random.Range(-5f, 5f);

        if ((x > -2 && x < 2) && (y > -2 && y < 2))
        {
            x = 3f;
            y = 3f;
        }

        transform.position = AttackTarget.position + new Vector3(x, y, 0f);
    }
}