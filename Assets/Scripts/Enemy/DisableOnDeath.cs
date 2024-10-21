using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnDeath : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Rigidbody2D rb;
    private Animator anim;
    private EnemyShootingController enemyShootingController;
    private DodgeMovement dodgeMovement;
    private EnemyController enemyController;


    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        rb = GetComponent<Rigidbody2D>();
        dodgeMovement = GetComponent<DodgeMovement>();
        enemyController = GetComponent<EnemyController>();
        enemyShootingController = GetComponent<EnemyShootingController>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        healthSystem.OnDeath += OnDeath;
        anim.enabled = false;
    }

    private void OnDisable()
    {
        healthSystem.OnDeath -= OnDeath;
        if (dodgeMovement != null)
        {
            dodgeMovement.enabled = true;
        }
        enemyController.enabled = true;
        enemyShootingController.IsDead = false;
    }

    private void OnDeath(Vector2 position)
    {
        if (dodgeMovement != null)
        {
            dodgeMovement.enabled = false;
        }
        enemyController.enabled = false;
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
        gameObject.SetActive(false);
    }
}
