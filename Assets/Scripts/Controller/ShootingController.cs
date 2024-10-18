using System;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private Transform pivot;

    private DodgeController controller;
    private StatHandler statHandler;

    private Vector2 target = Vector2.zero;
    private Vector2 aim = Vector2.zero;
    private string bulletTag = "PlayerBasicBullet";

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
        //����ü ����
        CreateProjectile();
    }

    private void CreateProjectile()
    {
        switch (transform.tag)
        {
            case "Player":
                bulletTag = "HomingBullet";
                target = aim;
                break;
            case "BasicEnemy":
                bulletTag = "EnemyBasicBullet";
                break;
            case "HomingEnemy":
                bulletTag = "EnemyHomingBullet";
                break;

        }
        //spread�ǰ�� ������Ʈ�� �������� , pivot.position �߰� + setactive false��
        GameObject projectile = GameManager.Instance.objPool.GetObjectFromPool(bulletTag, pivot.position);

        Bullet bullet = projectile.GetComponent<Bullet>();
        bullet.SetShooter(this.gameObject);

        if(bullet != null)
        {
            //spread�� ��� target�� pivot.position �������� aim
            bullet.Move(statHandler.CurrentStat.bulletSpeed, aim);
        }
    }

    private void OnAim(Vector2 direction)
    {
        aim = direction;
    }
}