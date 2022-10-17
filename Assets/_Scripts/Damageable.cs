using UnityEngine.UI;
using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float tempHealthAnimSpeed;

    [Header("UI")]
    [SerializeField] protected Image healthImage;
    [SerializeField] protected Image tempHealthImage;

    [Header("Effects")]
    [SerializeField] protected FloatingText floatingText;
    [SerializeField] protected ParticleSystem damageEffect;
    [SerializeField] protected float damageEffectDestroyTimer;

    protected float health;

    protected virtual float Health
    {
        get { return health; }
        set
        {
            health = value;

            if (health > maxHealth)
                health = maxHealth;
            else if (health <= 0)
                Die();

            healthImage.fillAmount = health / maxHealth;
        }
    }

    protected virtual void OnEnable()
    {
        Health = maxHealth;
    }

    protected virtual void FixedUpdate()
    {
        if (tempHealthImage != null)
            tempHealthImage.fillAmount = Mathf.Lerp(tempHealthImage.fillAmount, healthImage.fillAmount, tempHealthAnimSpeed * Time.fixedDeltaTime);
    }

    protected virtual void CreateDamageEffect()
    {
        var effect = Instantiate(damageEffect, transform.position, Quaternion.identity, Environment.Instance.trashParent);
        Destroy(effect, damageEffectDestroyTimer);
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;

        CreateDamageEffect();
    }

    public virtual void TakeDamage(float damage, float pushForce)
    {
        TakeDamage(damage);
    }
}
