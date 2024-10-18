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
        thisBullet = GetComponent<Bullet>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet otherBullet = collision.GetComponent<Bullet>();

        //�Ѿ��� �� ��ü�� �°ԵǴ� ���
        if (otherBullet != null && (collision.gameObject == thisBullet.shooter || thisBullet.shooter == otherBullet.shooter))
        {
            Physics2D.IgnoreCollision(collision, thisCollider);
            return;
        }

        //���� Ȥ�� �÷��̾ �������
        if (IsLayerMatched(EnemyLayer.value, collision.gameObject.layer))
        {
            thisBullet.OnImpact(collision);
        }
        else if(IsLayerMatched(PlayerLayer.value, collision.gameObject.layer))
        {
            thisBullet.OnImpact(collision);
        }

        //�Ѿ˳��� �ε��� ���
        else if (IsLayerMatched(bulletLayer.value, collision.gameObject.layer))
        {
            thisBullet.DestroyProjectile();
        }
    }

    private bool IsLayerMatched(int layerMask, int layer)
    {
        return layerMask == (layerMask | 1 << layer);
    }
}
