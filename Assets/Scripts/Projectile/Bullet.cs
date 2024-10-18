using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public GameObject shooter { get; private set; }

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
        gameObject.SetActive(false);
    }
}

public enum BulletType
{
    StandardBullet,
    PierceBullet,
    HomingBullet,   //�ӵ��� 0.1��
    CircleBullet
}

