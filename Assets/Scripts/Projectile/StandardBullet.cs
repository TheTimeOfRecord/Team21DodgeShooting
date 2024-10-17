using System;
using System.Drawing;
using UnityEngine;

//일직선으로 날아감
public class StandardBullet : Bullet
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
}
