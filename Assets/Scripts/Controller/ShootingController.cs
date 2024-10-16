using System;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private GameObject[] projectilePrefabs;
    [SerializeField] private Transform pivot;

    private DodgeController controller;

    private Vector2 aim = Vector2.zero;
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
        //�ϴ� ù��° �������� �ִ� ������ ��ȯ
        GameObject projectile = Instantiate(projectilePrefabs[0], pivot.position, Quaternion.identity);
    }

    private void OnAim(Vector2 direction)
    {
        aim = direction;
    }
}