using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnDeath : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Rigidbody2D rigidbody;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        rigidbody = GetComponent<Rigidbody2D>();
        healthSystem.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        Debug.Log("Á×À½");
        rigidbody.velocity = Vector2.zero;
        
        // ÆÄ±«µÇ´Â Animation

        gameObject.SetActive(false);
    }
}
