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

        // 총알끼리 부딪힌 경우
        if (otherBullet != null)
        {
            HandleBulletCollision(otherBullet, collision);
        }
        // 총알이 오브젝트와 부딪힌 경우
        else
        {
            HandleObjectCollision(collision);
        }
    }

    // 총알끼리 충돌 처리
    private void HandleBulletCollision(Bullet otherBullet, Collider2D collision)
    {
        // 총알을 쏜 주체가 동일한 경우 충돌 무시
        if (thisBullet.shooter == otherBullet.shooter)
        {
            Physics2D.IgnoreCollision(collision, thisCollider);
        }
        else
        {
            Debug.Log("다른 사람이 쏜 총알끼리 부딪힌 경우, 총알이 파괴됨");
            thisBullet.DestroyProjectile();
        }
    }

    // 총알이 다른 오브젝트에 부딪힌 경우 처리
    private void HandleObjectCollision(Collider2D collision)
    {
        // 적군이 쏜 총알이 플레이어 오브젝트에 맞은 경우
        if (thisBullet.shooter.layer == EnemyLayer.value && IsLayerMatched(PlayerLayer.value, collision.gameObject.layer))
        {
            Debug.Log("적의 총알이 플레이어를 맞춤");
            thisBullet.OnImpact(collision);
        }
        // 플레이어가 쏜 총알이 적군 오브젝트에 맞은 경우
        else if (thisBullet.shooter.layer == PlayerLayer.value && IsLayerMatched(EnemyLayer.value, collision.gameObject.layer))
        {
            Debug.Log("플레이어의 총알이 적을 맞춤");
            thisBullet.OnImpact(collision);
        }
        else
        {
            // 그 외의 경우 충돌 무시
            Physics2D.IgnoreCollision(collision, thisCollider);
        }
    }

    private bool IsLayerMatched(int value, int layer)
    {
        return value == (value | 1 << layer);
    }
}
