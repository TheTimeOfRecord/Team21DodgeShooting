using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomingProjectile : MonoBehaviour
{
    public float speed = 5f;
    public string playerTag = "Player";
    private Transform target;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag(playerTag)?.transform;
        // 플레이어에 태그 추가 후 진행
        

    }
    private void FixedUpdate()
    {
        if (target == null)
        {
            
            rb.velocity = Vector2.down * speed;
            return;
        }

        Vector2 direction = (Vector2)target.transform.position - rb.position;
        
        rb.velocity = direction.normalized * speed;
        
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }



}
