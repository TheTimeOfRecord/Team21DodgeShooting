using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : DodgeController
{
    protected Transform AttackTarget { get; private set; }
    [SerializeField] private string targetTag = "Player";

    // ���� GameManager���� Player �޾ƿ�
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
}