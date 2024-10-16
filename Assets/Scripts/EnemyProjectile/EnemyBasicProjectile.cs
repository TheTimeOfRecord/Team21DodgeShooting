using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicProjectile : MonoBehaviour
{
    public float speed = 5f;

    public Vector2 direction = Vector2.down;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        rb.velocity = direction * speed;
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
