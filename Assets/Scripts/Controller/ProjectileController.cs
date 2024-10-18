using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask bulletLayer;
    [SerializeField] private LayerMask EnemyLayer;
    [SerializeField] private LayerMask PlayerLayer;

    private Bullet thisBullet;

    private Collider2D thisCollider;

    private void Awake()
    {
        thisCollider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        thisBullet = GetComponent<Bullet>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet otherBullet = collision.GetComponent<Bullet>();

        // �Ѿ˳��� �ε��� ���
        if (otherBullet != null)
        {
            HandleBulletCollision(otherBullet, collision);
        }
        // �Ѿ��� ������Ʈ�� �ε��� ���
        else
        {
            HandleObjectCollision(collision);
        }
    }

    // �Ѿ˳��� �浹 ó��
    private void HandleBulletCollision(Bullet otherBullet, Collider2D collision)
    {
        // �Ѿ��� �� ��ü�� ������ ��� �浹 ����
        if (thisBullet.shooter == otherBullet.shooter)
        {
            Physics2D.IgnoreCollision(collision, thisCollider);
        }
        else
        {
            Debug.Log("�ٸ� ����� �� �Ѿ˳��� �ε��� ���, �Ѿ��� �ı���");
            thisBullet.DestroyProjectile();
        }
    }

    // �Ѿ��� �ٸ� ������Ʈ�� �ε��� ��� ó��
    private void HandleObjectCollision(Collider2D collision)
    {
        // ������ �� �Ѿ��� �÷��̾� ������Ʈ�� ���� ���
        if (thisBullet.shooter.layer == EnemyLayer.value && IsLayerMatched(PlayerLayer.value, collision.gameObject.layer))
        {
            Debug.Log("���� �Ѿ��� �÷��̾ ����");
            thisBullet.OnImpact(collision);
        }
        // �÷��̾ �� �Ѿ��� ���� ������Ʈ�� ���� ���
        else if (thisBullet.shooter.layer == PlayerLayer.value && IsLayerMatched(EnemyLayer.value, collision.gameObject.layer))
        {
            Debug.Log("�÷��̾��� �Ѿ��� ���� ����");
            thisBullet.OnImpact(collision);
        }
        else
        {
            // �� ���� ��� �浹 ����
            Physics2D.IgnoreCollision(collision, thisCollider);
        }
    }

    private bool IsLayerMatched(int value, int layer)
    {
        return value == (value | 1 << layer);
    }
}
