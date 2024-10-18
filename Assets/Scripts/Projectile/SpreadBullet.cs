using System;
using UnityEngine;
using UnityEngine.UIElements;

//한 지점에서 퍼져나가는 총알. bulletCount를 조절하면 더 큰수, 작은수도 대응 가능
public class SpreadBullet : Bullet
{
    public int bulletCount = 20;
    private float bulletSpeed;
    private Vector2 spawnPoint;
    private float offsetDistance = 0.5f;

    private void OnEnable()
    {
        //아이템에 따라 총알 갯수가 나뉠 경우 다음과 같이 초기화 하면 될듯
        //bulletCount = shooter.GetComponent<StatHandler>().CurrentStat.bulletNum;
    }

    public override void Move(float speed, Vector2 piviotPosition)
    {
        bulletSpeed = speed;
        spawnPoint = piviotPosition;
        SpreadBulletFire();
    }


    private void SpreadBulletFire()
    {
        {
            float angleStep = 360f / bulletCount;
            float angle = 0f;

            for (int i = 0; i < bulletCount; i++)
            {
                float bulletDirX = Mathf.Sin((angle * Mathf.PI) / 180f);
                float bulletDirY = Mathf.Cos((angle * Mathf.PI) / 180f);

                Vector2 bulletVector = new Vector2(bulletDirX, bulletDirY);
                Vector2 bulletMoveDirection = bulletVector.normalized;

                Vector2 spawnPosition = spawnPoint + bulletMoveDirection * offsetDistance;
                GameObject projectiles = GameManager.Instance.objPool.GetObjectFromPool("SpreadBullet", spawnPosition);

                Rigidbody2D bulletRb = projectiles.GetComponent<Rigidbody2D>();

                projectiles.transform.up = bulletMoveDirection;

                //발사자 재설정
                Bullet bullet = projectiles.GetComponent<Bullet>();
                bullet.SetShooter(shooter);

                bulletRb.velocity = bulletMoveDirection * bulletSpeed;

                angle += angleStep;
            }
        }
    }
}