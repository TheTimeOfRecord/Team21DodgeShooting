using UnityEngine;

//플레이어를 계속해서 따라다니는 적군 전용 총알 = 프리팹 애니메이션 바꿔줘야함
public class HomingBullet : Bullet
{
    private Rigidbody2D rb;

    private Vector2 direction;
    private float bulletSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public override void Move(float speed, Vector2 target)
    {
        bulletSpeed = speed;

        direction = (GameManager.Instance.Player.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;
        transform.up = direction;

    }

    private void FixedUpdate()
    {
        Move(bulletSpeed, Vector2.one);
    }
}
