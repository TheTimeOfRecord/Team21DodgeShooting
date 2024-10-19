using System;
using System.Collections;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private Transform pivot;

    private DodgeController controller;
    private StatHandler statHandler;

    private Vector2 aim = Vector2.zero;

    private void Awake()
    {
        controller = GetComponent<DodgeController>();
        statHandler = GetComponent<StatHandler>();
    }

    private void Start()
    {
        controller.OnFireEvent += OnShoot;
        controller.OnLookEvent += OnAim;
    }

    private void OnShoot()
    {
        //투사체 생성
        SetProjectile();

    }

    private void SetProjectile()
    {
        Fire();
    }

    private void Fire()
    {
        GameObject projectile = GameManager.Instance.objPool.GetObjectFromPool("StandardBullet", pivot.position);

        Bullet bullet = projectile.GetComponent<Bullet>();
        bullet.SetShooter(this.gameObject);

        if (bullet != null)
        {
            bullet.Move(statHandler.CurrentStat.bulletSpeed, aim);
        }
    }


    private void OnAim(Vector2 direction)
    {
        aim = direction;
    }
}