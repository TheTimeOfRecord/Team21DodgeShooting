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
        healthSystem.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        rb.velocity = Vector2.zero;
        
        GameManager.Instance.EnemyDeathCount++;
        Debug.Log("Count = " + GameManager.Instance.EnemyDeathCount);

        // �ı��Ǵ� Animation

        gameObject.SetActive(false);
    }
}
