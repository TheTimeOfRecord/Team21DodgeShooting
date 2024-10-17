using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public GameObject shooter { get; private set; }

    //데미지처리, 움직임, 사이크조절 다 따로하는게 좋아보인다.
    public abstract void Move(float speed, Vector2 target);
    public virtual void OnImpact(Collider2D collision)
    {
        //해당 기체에 대미지를 준다.
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
    HomingBullet,   //속도의 0.1배
    CircleBullet
}

