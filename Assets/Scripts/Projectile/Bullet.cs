using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public abstract void Move(float speed, float damage);
    public abstract void OnImpact(Collider2D collision);
}

public enum BulletType
{
    StandardBullet,
    PierceBullet,
    HomingBullet,
    CircleBullet
}

