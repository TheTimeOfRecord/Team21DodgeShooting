using System;
using UnityEngine;

//관통형 투사체. 대상에게 부딪혀도 사라지지 않고 계속 움직임
public class PierceBullet : Bullet
{
    private float angleSpace = 5;

    public override void Move(float speed, Vector2 target)
    {
        gameObject.SetActive(false);
        float baseAngle = GetBaseAngle((target - (Vector2)transform.position).normalized);
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
            GameObject newBullet = GameManager.Instance.objPool.GetObjectFromPool("PierceBullet", this.transform.position);
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

    public override void OnImpact(Collider2D collision)
    {
        //데미지 처리는 하되 파괴되지는 않는
        HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
        StatHandler statHandler = collision.GetComponent<StatHandler>();
        healthSystem.ChangeHealth(statHandler.CurrentStat.ATK * -1);
    }

}
