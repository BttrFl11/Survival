using UnityEngine;

public class Weapon_0 : Weapon
{
    [SerializeField] private float rotationSpeed;

    protected override void FixedUpdate()
    {
        Attack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyStats enemyStats))
            GiveDamage(enemyStats);
    }

    public override void Attack()
    {
        transform.Rotate(Vector3.forward * rotationSpeed);
    }

    public override void GiveDamage(Damageable damageable)
    {
        damageable.TakeDamage(damage);
    }
}
