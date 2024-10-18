using System;
using System.Drawing;
using UnityEngine;

//일직선으로 날아감
public class StandardBullet : Bullet
{
    private float angleSpace = 5;

    public override void Move(float speed, Vector2 target)
    {
        gameObject.SetActive(false);
        float baseAngle = GetBaseAngle(target);
        float startAngle = GetStartAngle(baseAngle);
        FireBullets(speed, startAngle);
    }

    private float GetBaseAngle(Vector2 target)
    {
        return Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
    }

    private float GetStartAngle(float baseAngle)
    {
        return baseAngle - (bulletCount - 1) / 2f * angleSpace;
    }

    private void FireBullets(float speed, float startAngle)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = startAngle + i * angleSpace;

            // 총알 생성
            GameObject newBullet = GameManager.Instance.objPool.GetObjectFromPool("StandardBullet", this.transform.position);
            Bullet bulletComponent = newBullet.GetComponent<Bullet>();

            // 발사자 설정
            bulletComponent.SetShooter(shooter);

            // 방향 계산 및 총알 발사
            Vector2 direction = CalculateDirection(currentAngle);

            // 각도에 따른 회전 설정
            bulletComponent.transform.up = direction;

            bulletComponent.ShootBullet(speed, direction);  // ShootBullet 호출
        }
    }

    private Vector2 CalculateDirection(float angle)
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }
}
