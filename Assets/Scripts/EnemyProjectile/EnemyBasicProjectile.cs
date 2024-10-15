using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicProjectile : MonoBehaviour
{
    public float speed = 5f;
    
    public Vector2 direction = Vector2.down;
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
