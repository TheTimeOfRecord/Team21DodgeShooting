using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicProjectile : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        rb.velocity = Vector2.down * speed;
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
