using System.Collections;
using UnityEngine;

public class EnemyShootingController : MonoBehaviour
{
    private Transform pivot;

    private StatHandler statHandler;

    private Vector2 target = Vector2.zero;

    public bool IsDead { get; set; }

    private void Awake()
    {
        statHandler = GetComponent<StatHandler>();
        pivot = GetComponent<Transform>();
        IsDead = false;
    }


    private void OnEnable()
    {
        if (gameObject.activeSelf)
        {
            SetProjectile();
        }
    }

    private void SetProjectile()
    {
        if(!IsDead)
        switch (transform.tag)
        {
            case "StraightEnemy":
                BulletFire("HomingBullet");
                break;
            case "TracingEnemy":
                BulletFire("PierceBullet");
                break;
            case "HoveringEnemy":
                BulletFire("StandardBullet");
                break;
            case "BlinkingEnemy":
                SpreadBulletFire();
                break;
        }
    }

    private void SpreadBulletFire()
    {
        StartCoroutine(SpreadEnemyFire());
    }

    IEnumerator SpreadEnemyFire()
    { 
        while(!IsDead)
        {        //SpreadBullet일 경우에만
            GameObject spreadBulletProjectile = GameManager.Instance.objPool.GetObjectFromPool("SpreadBullet");

            Bullet spreadBullet = spreadBulletProjectile.GetComponent<Bullet>();
            spreadBullet.SetShooter(this.gameObject);

            if (spreadBullet != null)
            {
                spreadBullet.Move(statHandler.CurrentStat.bulletSpeed, pivot.position);
            }
            yield return WaitOneSecond;
        }
    }

    public void BulletFire(string bulletTag)
    {
        StartCoroutine(EnermyFire(bulletTag));
    }

    IEnumerator EnermyFire(string bulletTag)
    {
        while (!IsDead)
        {
            GameObject projectile = GameManager.Instance.objPool.GetObjectFromPool(bulletTag, pivot.position);

            Bullet bullet = projectile.GetComponent<Bullet>();
            bullet.SetShooter(this.gameObject);

            if (bullet != null)
            {
                target = GameManager.Instance.Player.position;
                bullet.SetShooter(this.gameObject);
                bullet.Move(statHandler.CurrentStat.bulletSpeed, target);
            }
            yield return WaitOneSecond;
        }
    }

    WaitForSeconds WaitOneSecond = new WaitForSeconds(1f);
}