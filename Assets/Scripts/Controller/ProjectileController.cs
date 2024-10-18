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

        //�Ѿ˳��� �ε��� ���
        if(otherBullet != null)
        {
            //�Ѿ��� �� ��ü�� ������ ���
            if(thisBullet.shooter == otherBullet.shooter)
            {
                Physics2D.IgnoreCollision(collision, thisCollider);
                return;
            }
            else
            {
                //�ٸ������ �� �Ѿ˳��� �ε������ �����
                thisBullet.DestroyProjectile();
            }
        }

        //�Ѿ��� ������Ʈ�� ���� ���
        else
        {
            //�Ѿ��� �� ��ü�� ������
            if(collision.gameObject == thisBullet.shooter)
            {
                //�浹�� ����
                Physics2D.IgnoreCollision(collision, thisCollider);
                return;
            }
            //���� Ȥ�� �÷��̾ �������
            if (IsLayerMatched(EnemyLayer.value, collision.gameObject.layer))
            {
                thisBullet.OnImpact(collision);
            }
            else if (IsLayerMatched(PlayerLayer.value, collision.gameObject.layer))
            {
                thisBullet.OnImpact(collision);
            }
        }
    }

    private bool IsLayerMatched(int layerMask, int layer)
    {
        return layerMask == (layerMask | 1 << layer);
    }
}
