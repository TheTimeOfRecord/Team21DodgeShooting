using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask bulletLayer;
    [SerializeField] private LayerMask EnemyLayer;
    [SerializeField] private LayerMask PlayerLayer;

    private Collider2D thisCollider;

    private void Awake()
    {
        thisCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = GetComponent<Bullet>();

        //�Ѿ��� �� ��ü�� �°ԵǴ� ���
        if (collision.gameObject == bullet.shooter)
        {
            Physics2D.IgnoreCollision(collision, thisCollider);
            return;
        }

        //���� Ȥ�� �÷��̾ �������
        if (IsLayerMatched(EnemyLayer.value, collision.gameObject.layer))
        {
            bullet.OnImpact(collision);
        }
        else if(IsLayerMatched(PlayerLayer.value, collision.gameObject.layer))
        {
            bullet.OnImpact(collision);
        }

        //�Ѿ˳��� �ε��� ���
        else if (IsLayerMatched(bulletLayer.value, collision.gameObject.layer))
        {
            bullet.DestroyProjectile();
        }
    }

    private bool IsLayerMatched(int layerMask, int layer)
    {
        return layerMask == (layerMask | 1 << layer);
    }
}
