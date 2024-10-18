using System;
using UnityEngine;

//관통형 투사체. 대상에게 부딪혀도 사라지지 않고 계속 움직임
public class PierceBullet : Bullet
{
    private Rigidbody2D rb;

    private Vector2 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public override void Move(float speed, Vector2 target)
    {
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        direction = target.normalized * speed;
    }

    private void FixedUpdate()
    {
        ApplyMove();
    }

    private void ApplyMove()
    {
        rb.velocity = direction;
    }

    public override void OnImpact(Collider2D collision)
    {
        //데미지 처리는 하되 파괴되지는 않는
        HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
        StatHandler statHandler = GetComponent<StatHandler>();
        healthSystem.ChangeHealth(statHandler.CurrentStat.ATK);
    }

}
