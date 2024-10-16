using System;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    //�������� ������ �����̵�

    //���� : ��� ����ü�� ����ϸ�, ��¦�Ÿ��� �ִϸ��̼��� ����

    [SerializeField] private Collider2D collider;

    private HealthSystem healthSystem;


    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        healthSystem.OnDamage += BecomeInvincible;
        healthSystem.OnInvincibilityEnd += EndInvincibility;
    }

    private void BecomeInvincible()
    {
        collider.enabled = false;
        //��¦�̴� ����Ʈ ����
    }

    private void EndInvincibility()
    {
        collider.enabled = true;
        //��¦�̴� ����Ʈ ����
    }
}