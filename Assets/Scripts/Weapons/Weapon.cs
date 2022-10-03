using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected float damage;

    protected virtual void FixedUpdate()
    {
        Attack();
    }

    public abstract void Attack();
    public abstract void GiveDamage(Damageable damageable);
}
