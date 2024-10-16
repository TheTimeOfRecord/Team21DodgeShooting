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
        //투사체 생성
        CreateProjectile();
    }

    private void CreateProjectile()
    {
        //일단 첫번째 프리팹의 있는 아이템 소환
        GameObject projectile = Instantiate(projectilePrefabs[0], pivot.position, Quaternion.identity);
    }

    private void OnAim(Vector2 direction)
    {
        aim = direction;
    }
}