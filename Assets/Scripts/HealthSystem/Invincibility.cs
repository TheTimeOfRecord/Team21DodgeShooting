using System;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    //�������� ������ �����̵�

    //���� : ��� ����ü�� ����ϸ�, ��¦�Ÿ��� �ִϸ��̼��� ����

    [SerializeField] private Collider2D hitCollider;

    private HealthSystem healthSystem;


    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        hitCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        healthSystem.OnDamage += BecomeInvincible;
        healthSystem.OnInvincibilityEnd += EndInvincibility;
    }

    private void BecomeInvincible()
    {
        hitCollider.enabled = false;
        //��¦�̴� ����Ʈ ����
    }

    private void EndInvincibility()
    {
        hitCollider.enabled = true;
        //��¦�̴� ����Ʈ ����
    }
}