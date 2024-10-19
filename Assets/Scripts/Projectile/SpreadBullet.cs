using System;
using UnityEngine;
using UnityEngine.UIElements;

//한 지점에서 퍼져나가는 총알. bulletCount를 조절하면 더 큰수, 작은수도 대응 가능
public class SpreadBullet : Bullet
{
    private float bulletSpeed;
    private float offsetDistance = 0.5f;


    public override void Move(float speed, Vector2 piviotPosition)
    {
        gameObject.SetActive(false);
        bulletSpeed = speed;
        SpreadBulletFire(piviotPosition);
    }


    private void SpreadBulletFire(Vector2 pivoit)
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

                Vector2 spawnPosition = pivoit + bulletMoveDirection * offsetDistance;
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