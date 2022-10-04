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

    public abstract void Attack();
    public abstract void GiveDamage(Damageable damageable);
}
