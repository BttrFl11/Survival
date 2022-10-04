using UnityEngine.UI;
using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] protected float maxHealth;
    [SerializeField] protected Image healthImage;
    [SerializeField] protected Image tempHealthImage;
    [SerializeField] protected float tempHealthAnimSpeed;
    [SerializeField] protected FloatingText floatingText;

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

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;

        GameObject createdText = Instantiate(floatingText.gameObject, transform.position, Quaternion.identity);
        var floatingTXT = createdText.GetComponent<FloatingText>();
        floatingTXT.SetText(damage.ToString("0"));
    }
}
