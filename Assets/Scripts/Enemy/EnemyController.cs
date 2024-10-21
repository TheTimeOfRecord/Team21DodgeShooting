using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : DodgeController
{
    protected Transform AttackTarget { get; private set; }
    protected Vector2 direction;
    [SerializeField] protected Sprite sprite;
    protected SpriteRenderer mainSprite;

    protected override void Awake()
    {
        base.Awake();
        mainSprite = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void OnEnable()
    {
        mainSprite.sprite = sprite;
        AttackTarget = GameManager.Instance.Player;
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

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
            StatHandler statHandler = GetComponent<StatHandler>();
            healthSystem.ChangeHealth(statHandler.CurrentStat.ATK * -1);
            Debug.Log("Ãæµ¹! " + healthSystem.CurrentHealth);
            gameObject.SetActive(false);
        }
    }
}