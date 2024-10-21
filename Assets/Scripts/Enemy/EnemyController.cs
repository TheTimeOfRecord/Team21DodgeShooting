using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : DodgeController
{
    protected Transform AttackTarget { get; private set; }
    protected Vector2 direction;
    protected StatHandler statHandler;
    [SerializeField] protected Sprite sprite;
    protected SpriteRenderer mainSprite;

    // [SerializeField] private string targetTag = "Player";

    protected override void Awake()
    {
        base.Awake();
        AttackTarget = GameManager.Instance.Player;
        mainSprite = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void OnEnable()
    {
        mainSprite.sprite = sprite;
    }

    protected virtual void Start()
    {
        SceneManager.sceneLoaded += UpdateTarget;
    }

    private void UpdateTarget(Scene arg0, LoadSceneMode arg1)
    {
        AttackTarget = GameManager.Instance.Player;
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

    protected void RotateToTarget(Vector2 _direction)
    {
        float rotZ = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ - 90f);
    }
}