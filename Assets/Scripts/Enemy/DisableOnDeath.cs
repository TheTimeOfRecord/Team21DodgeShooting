using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnDeath : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Rigidbody2D rb;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        rb = GetComponent<Rigidbody2D>();
        healthSystem.OnDeath -= OnDeath;

        healthSystem.OnDeath += OnDeath;
    }

    

    private void OnDeath(Vector2 position)
    {
        rb.velocity = Vector2.zero;
        
        GameManager.Instance.EnemyDeathCount++;
        Debug.Log("Count = " + GameManager.Instance.EnemyDeathCount);

        // ÆÄ±«µÇ´Â Animation

        healthSystem.ResetHealth();
        gameObject.SetActive(false);

    }
}
