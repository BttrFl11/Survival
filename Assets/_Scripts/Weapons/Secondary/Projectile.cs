using UnityEngine;

public class Projectile : SecondaryWeapon
{
    protected float speed;
    protected float lifetime;
    protected Vector2 direction;

    protected Vector2 Position
    {
        get => transform.position;
        set => transform.position = value;
    }

    public virtual void Initialize(float d, float s, float l, Vector2 dir)
    {
        damage = d;
        speed = s;
        lifetime = l;
        direction = dir;
    }

    private void Start()
    {
        Invoke(nameof(DestroyProjectile), lifetime);
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        transform.Translate(speed * Time.fixedDeltaTime * direction);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyStats enemyStats))
            GiveDamage(enemyStats);
    }
}
