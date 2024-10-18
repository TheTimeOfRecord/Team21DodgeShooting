﻿using UnityEngine;

//플레이어를 계속해서 따라다니는 적군 전용 총알 = 프리팹 애니메이션 바꿔줘야함
public class HomingBullet : Bullet
{
    private Vector2 direction;
    private float bulletSpeed;

    public override void Move(float speed, Vector2 target)
    {
        bulletSpeed = speed;

        direction = (GameManager.Instance.Player.position - transform.position).normalized;

        transform.up = direction;

    }

    private void FixedUpdate()
    {
        rb.velocity = direction * bulletSpeed;
    }
}
