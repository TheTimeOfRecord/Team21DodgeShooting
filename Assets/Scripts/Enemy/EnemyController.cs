using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : DodgeController
{
    protected Transform AttackTarget { get; private set; }
    [SerializeField] private string targetTag = "Player";
    
    // 추후 Stat의 Speed로 변환 예정
    [SerializeField] protected float speed;

    // 추후 GameManager에서 Player 받아옴
    public Transform PlayerTransform;

    protected override void Awake()
    {
        base.Awake();
        PlayerTransform = GameObject.FindGameObjectWithTag(targetTag).transform;
    }

    protected virtual void Start()
    {
        AttackTarget = PlayerTransform;
        // AttackTarget = GameManager.Instance.Player;
    }

    protected virtual void FixedUpdate()
    {

    }

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, AttackTarget.position);
    }

    protected Vector2 DirectionToTarget()
    {
        return (AttackTarget.position - transform.position).normalized;
    }

    protected void RotateToTarget(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, 0, rotZ - 90f);
    }
}