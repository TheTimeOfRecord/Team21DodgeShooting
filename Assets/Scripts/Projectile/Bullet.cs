using System;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public GameObject shooter { get; private set; }
    public int bulletCount = 1;
    public float size = 1;
    protected Rigidbody2D rb;

    public abstract void Move(float speed, Vector2 target);
    public virtual void OnImpact(Collider2D collision)
    {
        //해당 기체에 대미지를 준다.
        HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
        StatHandler statHandler = collision.GetComponent<StatHandler>();
        healthSystem.ChangeHealth(statHandler.CurrentStat.ATK * -1);
        DestroyProjectile();
    }

    private void OnEnable()
    {
        if(shooter != null)
        {
            bulletCount = shooter.GetComponent<StatHandler>().CurrentStat.bulletNum;
            size = shooter.GetComponent<StatHandler>().CurrentStat.bulletSize;
            SetBulletSize(size);
        }
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetShooter(GameObject _shooter)
    {
        shooter = _shooter;
        bulletCount = shooter.gameObject.GetComponent<StatHandler>().CurrentStat.bulletNum;
    }

    public void SetBulletSize(float size)
    {
        transform.localScale = Vector3.one * size;
    }

    public void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }

    public void SetDirection(Vector2 vector2)
    {
        rb.velocity = vector2;
    }

    public void ShootBullet(float speed, Vector2 direction)
    {
        rb.velocity = direction.normalized * speed;
    }
}

public enum BulletType
{
    StandardBullet,
    PierceBullet,
    HomingBullet,   //속도의 0.1배
    CircleBullet
}

