using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnDeath : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Rigidbody2D rb;
    private Animator anim;
    private EnemyShootingController enemyShootingController;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        enemyShootingController = GetComponent<EnemyShootingController>();

        anim.enabled = false;
        healthSystem.OnDeath -= OnDeath;

        healthSystem.OnDeath += OnDeath;
    }

    private void OnEnable()
    {
        anim.enabled = false;
    }

    private void OnDeath(Vector2 position)
    {
        rb.velocity = Vector2.zero;
        
        GameManager.Instance.EnemyDeathCount++;
        Debug.Log("Count = " + GameManager.Instance.EnemyDeathCount);

        enemyShootingController.IsDead = true;        
        anim.enabled = true;
        anim.SetTrigger("Dead");
        Invoke("OnDeathInvoke", 1.5f);
    }

    private void OnDeathInvoke()
    {
        healthSystem.ResetHealth();
        enemyShootingController.IsDead = false;
        gameObject.SetActive(false);
    }
}
