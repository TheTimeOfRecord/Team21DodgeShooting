using System;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private Transform pivot;

    private DodgeController controller;

    private Vector2 aim = Vector2.zero;
    private string bulletTag = "PlayerBasicBullet";

    private void Awake()
    {
        controller = GetComponent<DodgeController>();
    }

    private void Start()
    {
        controller.OnFireEvent += OnShoot;
        controller.OnLookEvent += OnAim;
    }

    private void OnShoot()
    {
        //투사체 생성
        CreateProjectile();
    }

    private void CreateProjectile()
    {
        switch (transform.tag)
        {
            case "Player":
                bulletTag = "PlayerBasicBullet";
                break;
            case "BasicEnemy":
                bulletTag = "EnemyBasicBullet";
                break;
            case "HomingEnemy":
                bulletTag = "EnemyHomingBullet";
                break;

        }
        //총알 생성 => 어떤 종류의 총알을 발사할 것인가?
        GameObject projectile = GameManager.Instance.objPool.GetObjectFromPool(bulletTag);
        projectile.transform.position = pivot.position;
    }

    private void OnAim(Vector2 direction)
    {
        aim = direction;
    }
}