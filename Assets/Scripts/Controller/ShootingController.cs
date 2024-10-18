using System;
using System.Collections;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private Transform pivot;

    private DodgeController controller;
    private StatHandler statHandler;

    private Vector2 target = Vector2.zero;
    private Vector2 aim = Vector2.zero;
    private string bulletTag = "StandardBullet";

    private void Awake()
    {
        controller = GetComponent<DodgeController>();
        statHandler = GetComponent<StatHandler>();
    }

    private void Start()
    {
        controller.OnFireEvent += OnShoot;
        controller.OnLookEvent += OnAim;

        if(!(transform.tag == "Player"))
        {
            StartCoroutine(EnermyFire());
        }
    }

    private void OnShoot()
    {
        //투사체 생성
        SetProjectile();

    }

    private void SetProjectile()
    {
        switch (transform.tag)
        {
            case "Player":
                bulletTag = "SpreadBullet";
                target = aim;
                break;
            case "StraighEnemy":
                bulletTag = "HomingBullet";
                break;
            case "TracingEnemy":
                bulletTag = "StandardBullet";
                break;
            case "HoveringEnemy":
                bulletTag = "StandardBullet";
                break;
            case "BlinkingEnemy":
                bulletTag = "SpreadBullet";
                break;
        }

        Fire();
    }

    private void Fire()
    {
        if (bulletTag == "SpreadBullet")
        {
            //SpreadBullet일 경우에만
            GameObject spreadBulletProjectile = GameManager.Instance.objPool.GetObjectFromPool(bulletTag);

            Bullet spreadBullet = spreadBulletProjectile.GetComponent<Bullet>();
            spreadBullet.SetShooter(this.gameObject);

            if (spreadBullet != null)
            {
                spreadBullet.Move(statHandler.CurrentStat.bulletSpeed, pivot.position);
            }

        }
        else
        {
            GameObject projectile = GameManager.Instance.objPool.GetObjectFromPool(bulletTag, pivot.position);

            Bullet bullet = projectile.GetComponent<Bullet>();
            bullet.SetShooter(this.gameObject);

            if (bullet != null)
            {
                bullet.Move(statHandler.CurrentStat.bulletSpeed, target);
            }
        }
    }

    IEnumerator EnermyFire()
    {
        while (transform.gameObject.activeSelf == true)
        {
            SetProjectile();
            yield return WaitOneSecond;
        }
    }

    WaitForSeconds WaitOneSecond = new WaitForSeconds(1f);

    private void OnAim(Vector2 direction)
    {
        aim = direction;
    }
}