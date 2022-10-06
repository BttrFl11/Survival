using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage;
    private float speed;
    private float lifetime;
    private Vector2 direction;

    public void Initialize(float d, float s, float l, Vector2 dir)
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

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(speed * Time.fixedDeltaTime * direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyStats enemyStats))
            GiveDamage(enemyStats);
    }

    private void GiveDamage(EnemyStats enemyStats)
    {
        enemyStats.TakeDamage(damage);
        //DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        Debug.Log("Projectile was destoyed");

        Destroy(gameObject);
    }
}
