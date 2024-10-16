using System;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    //데미지를 입으면 무적이됨

    //무적 : 모든 투사체를 통과하며, 반짝거리는 애니메이션이 보임

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
        //반짝이는 이펙트 켜짐
    }

    private void EndInvincibility()
    {
        hitCollider.enabled = true;
        //반짝이는 이펙트 꺼짐
    }
}