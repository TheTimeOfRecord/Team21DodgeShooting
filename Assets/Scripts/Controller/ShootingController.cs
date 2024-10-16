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
        //����ü ����
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
        //�Ѿ� ���� => � ������ �Ѿ��� �߻��� ���ΰ�?
        GameObject projectile = GameManager.Instance.objPool.GetObjectFromPool(bulletTag);
        projectile.transform.position = pivot.position;
    }

    private void OnAim(Vector2 direction)
    {
        aim = direction;
    }
}