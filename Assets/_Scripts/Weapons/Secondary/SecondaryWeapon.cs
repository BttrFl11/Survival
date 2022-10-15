using UnityEngine;

public class SecondaryWeapon : MonoBehaviour
{
    protected float damage;

    public virtual void Initialize(float d)
    {
        damage = d;
    }

    protected virtual void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    protected virtual void GiveDamage(EnemyStats enemyStats)
    {
        enemyStats.TakeDamage(damage);
    }
}
