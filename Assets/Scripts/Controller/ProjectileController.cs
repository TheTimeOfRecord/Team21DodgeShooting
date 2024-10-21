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
        if(collision == null)
        {
            return;
        }
        else
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
    }

    // �Ѿ˳��� �浹 ó��
    private void HandleBulletCollision(Bullet otherBullet, Collider2D collision)
    {
        // �Ѿ��� �� ��ü�� ������ ��� �浹 ����
        if (thisBullet.shooter == otherBullet.shooter)
        {
            Physics2D.IgnoreCollision(collision, thisCollider);
        }
        // ���� �� �Ѿ˳��� �ε��� ��� �浹 ����
        else if(IsLayerMatched(EnemyLayer.value, thisBullet.shooter.layer) && IsLayerMatched(EnemyLayer.value, otherBullet.shooter.layer))
        {
            Physics2D.IgnoreCollision(collision, thisCollider);
        }
        else
        {
            thisBullet.DestroyProjectile();
        }
    }

    // �Ѿ��� �ٸ� ������Ʈ�� �ε��� ��� ó��
    private void HandleObjectCollision(Collider2D collision)
    {
        if(thisBullet.shooter == null)
        {
            Debug.Log("thisbullet.shooter is null" + thisBullet);
        }
        // ������ �� �Ѿ��� �÷��̾� ������Ʈ�� ���� ���
        else if (IsLayerMatched(EnemyLayer.value, thisBullet.shooter.layer) && IsLayerMatched(PlayerLayer.value, collision.gameObject.layer))
        {
            thisBullet.OnImpact(collision);
        }
        // �÷��̾ �� �Ѿ��� ���� ������Ʈ�� ���� ���
        else if (IsLayerMatched(PlayerLayer.value, thisBullet.shooter.layer) && IsLayerMatched(EnemyLayer.value, collision.gameObject.layer))
        {
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
