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

        //총알끼리 부딪힌 경우
        if(otherBullet != null)
        {
            //총알을 쏜 주체가 동일할 경우
            if(thisBullet.shooter == otherBullet.shooter)
            {
                Physics2D.IgnoreCollision(collision, thisCollider);
                return;
            }
            else
            {
                //다른사람이 쏜 총알끼리 부딪힌경우 사라짐
                thisBullet.DestroyProjectile();
            }
        }

        //총알이 오브젝트를 맞출 경우
        else
        {
            //총알을 쏜 주체를 맞출경우
            if(collision.gameObject == thisBullet.shooter)
            {
                //충돌을 무시
                Physics2D.IgnoreCollision(collision, thisCollider);
                return;
            }
            //상대방 혹은 플레이어가 맞을경우
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
