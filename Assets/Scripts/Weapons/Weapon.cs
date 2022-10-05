using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected float damageScale;

    public static PlayerFighting playerFighting;

    protected virtual void OnEnable()
    {
        if (playerFighting == null)
            playerFighting = FindObjectOfType<PlayerFighting>();
    }

    protected virtual void FixedUpdate()
    {
        Attack();
    }

    public virtual void GiveDamage(Damageable damageable)
    {
        float damage = damageScale * playerFighting.Damage;
        damageable.TakeDamage(damage);
    }

    public abstract void Attack();
}
