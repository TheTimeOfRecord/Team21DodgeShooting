using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerProjectileController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction = new Vector2 (0,1);
    public float speed = 5; // 나중에 삭제
    private bool isReady;

    [SerializeField] private LayerMask levelCollisionLayer;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        isReady = true;
    }

    
    void Update()
    {
        if (!isReady)
        {
            return;
        }

        rb.velocity = direction * speed;
    }
    public void InitializedAttack(Vector2 direction)
    {
        this.direction = direction;


        isReady = true;
    }

    private void UpdateProjectileSprite()
    {
        //transform.localScale = Vector3.one * attackData.size;
        //크기조절
    }


    void OnBecameInvisible()
    {
        Destroy(this);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsLayerMatched(levelCollisionLayer.value, collision.gameObject.layer))
        {
            Vector2 destroyPosition = collision.ClosestPoint(transform.position) - direction * 0.2f;
            DestoryProjectile(destroyPosition);

        }
        //else if (IsLayerMatched(attackData.target.value, collision.gameObject.layer))
        //{
        //    
        //    DestoryProjectile(collision.ClosestPoint(transform.position));
        //}


    }

    private void DestoryProjectile(Vector3 position)
    {
        gameObject.SetActive(false);
    }

    private bool IsLayerMatched(int value, int layer)
    {
        return value == (value | 1 << layer);
    }
}
