using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected float damageScale;
    [SerializeField] protected float pushForce;

    public static PlayerFighting playerFighting;

    public WeaponType WeaponType;

    protected float Damage
    {
        get => damageScale * playerFighting.Damage;
    }

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
        damageable.TakeDamage(Damage, pushForce);
    }

    // Called once in per frame
    public abstract void Attack();
}
