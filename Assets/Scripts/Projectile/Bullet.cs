using System;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public GameObject shooter { get; private set; }
    protected Rigidbody2D rb;

    //������ó��, ������, ����ũ���� �� �����ϴ°� ���ƺ��δ�.
    public abstract void Move(float speed, Vector2 target);
    public virtual void OnImpact(Collider2D collision)
    {
        //�ش� ��ü�� ������� �ش�.
        HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
        StatHandler statHandler = GetComponent<StatHandler>();
        healthSystem.ChangeHealth(statHandler.CurrentStat.ATK);
        DestroyProjectile();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetShooter(GameObject _shooter)
    {
        shooter = _shooter;
    }

    public void SetBulletSize(float size)
    {
        transform.localScale = Vector3.one * size;
    }

    public void DestroyProjectile()
    {
        Debug.Log("�Ѿ� �ı���");
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
    HomingBullet,   //�ӵ��� 0.1��
    CircleBullet
}

